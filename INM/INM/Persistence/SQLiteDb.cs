using System.Collections.Generic;
using INM.Models;
using System.Linq;
using System.Security.Cryptography;

namespace INM.Persistence
{
    public class SQLiteDb : ISQLiteDb, System.IDisposable
    {
        private static SQLite.SQLiteConnection _DbConnection { get; set; } = null;

        public SQLiteDb()
        {
           
            string applicationFolderPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "persistence");
            if (!System.IO.Directory.Exists(applicationFolderPath))
            {
                System.IO.Directory.CreateDirectory(applicationFolderPath);
            }
            string databaseFilePath = System.IO.Path.Combine(applicationFolderPath, "inmDB.sqlite");
            _DbConnection = new SQLite.SQLiteConnection(databaseFilePath);

            #region CreateDbTables

            _DbConnection.CreateTable<User>(); // users
            _DbConnection.CreateTable<UserUser>(); // contacts
            _DbConnection.CreateTable<Group>(); // groups
            _DbConnection.CreateTable<GroupUser>(); // groups with users
            _DbConnection.CreateTable<GroupAudioRecord>(); // groups with audio records
            _DbConnection.CreateTable<Transcript>(); // audio text
            _DbConnection.CreateTable<AudioRecord>(); // audio data
            #endregion
        }

        public bool GetContactList(User user)
        {
            try
            {
                var primaryUserList = _DbConnection.Table<UserUser>().Where(x => x.PrimaryUserId == user.ID || x.ContactUserId == user.ID).ToList();
                foreach (UserUser uu in primaryUserList)
                {
                    user.Contacts.Add(uu.PrimaryUserId == user.ID? GetUserById(uu.ContactUserId): GetUserById(uu.PrimaryUserId));
                }
                return true;
            }
            catch (System.InvalidOperationException ioe)
            {
                System.Console.WriteLine(ioe.StackTrace);
                return false;
            }
        }
        public bool CreateGroup(Group newGroup)
        {
            if (!CheckGroupExists(newGroup.GroupName))
            {
                try
                {
                    _DbConnection.Insert(newGroup, typeof(Group));
                }
                catch (System.InvalidOperationException ioe)
                {
                    System.Console.WriteLine(ioe.StackTrace);
                    return false;
                }
            }

            return true;
        }

        public bool CreateRecording(AudioRecord newAudio)
        {
            try
            {
                _DbConnection.Insert(newAudio, typeof(AudioRecord));
                return true;
            }
            catch (System.InvalidOperationException ioe)
            {
                System.Console.WriteLine(ioe.StackTrace);
                return false;
            }
        }

        public bool CreateContact(int user1Id, int user2Id)
        {
            
            if (GetContact(user1Id, user2Id) == null)
            {
                int size = _DbConnection.Table<UserUser>().Count() + 1;
                UserUser newCont = new UserUser(user1Id, user2Id, size);

                try
                {
                    _DbConnection.Insert(newCont, typeof(UserUser));
                    return true;
                }
                catch (System.InvalidOperationException ioe)
                {
                    System.Console.WriteLine(ioe.StackTrace);
                    return false;
                }
            }

            return false;
        }

        public bool CreateUser(User newUser)
        {
            // check if user exists
            var userByEmail = GetUserByEmail(newUser.Email);
            var userByUsername = GetUserByUsername(newUser.Username);

            // if a user is not already found
            if (userByEmail == null && userByUsername == null)
            {
                // add the new user
                _DbConnection.Insert(newUser);

                // return success
                return true;
            }
            // there was a user found
            else
            {
                // user matched on username, complain
                if (userByUsername != null)
                {
                    throw new System.ArgumentException("The requested UserName is already taken!", "Models.User.UserName");
                }
                // user matched on email, complain
                else if (userByEmail != null)
                {
                    throw new System.ArgumentException("The given email has already been used!", "Models.User.Email");
                }
                // unknown issue
                else
                {
                    throw new System.ArgumentException("Unhandled exception in 'SQLiteDb.CreateUser()'", "Unknown Parameter Issue");
                }
            }

        }


        public bool DeleteContact(int lowerUserId, int upperUserId)
        {
            try
            {
                UserUser delCont = GetContact(lowerUserId, upperUserId);
                _DbConnection.Delete<UserUser>(delCont.ID);
                return true;
            }
            catch (System.InvalidOperationException ioe)
            {
                System.Console.WriteLine(ioe.StackTrace);
                return false;
            }
        }

        public bool DeleteGroup(int groupId)
        {
            try
            {
                _DbConnection.Delete<Group>(groupId);
                return true;
            }
            catch (System.InvalidOperationException ioe)
            {
                System.Console.WriteLine(ioe.StackTrace);
                return false;
            }
        }


        public bool UpdateGroup(Group groupToUpdate)
        {
            try
            {
                _DbConnection.Update(groupToUpdate);
                return true;
            }
            catch (System.InvalidOperationException ioe)
            {
                System.Console.WriteLine(ioe.StackTrace);
                return false;
            }
        }

        public bool UpdateRecording(AudioRecord recordingToUpdate)
        {
            try
            {
                _DbConnection.Update(recordingToUpdate);
                return true;
            }
            catch (System.InvalidOperationException ioe)
            {
                System.Console.WriteLine(ioe.StackTrace);
                return false;
            }
        }

        public bool UpdateUser(User userToUpdate)
        {
            try
            {
                _DbConnection.Update(userToUpdate);
                return true;
            }
            catch (System.InvalidOperationException ioe)
            {
                System.Console.WriteLine(ioe.StackTrace);
                return false;
            }
        }


        public List<Group> GetUserGroups(int userId)
        {
            return _DbConnection.Table<Group>().Where(g => g.LeadUserId == userId).ToList();
        }

        public List<User> GetUsers()
        {
            return _DbConnection.Table<User>().Select(u => u).ToList();
        }

        public User GetUserByUsername(string userName)
        {
            try
            {
                return _DbConnection.Table<User>().Where(x => x.Username == userName).First();
            }
            catch (System.InvalidOperationException ioe)
            {
                System.Console.WriteLine(ioe.StackTrace);
                return null;
            }
        }

        public User GetUserByEmail(string email)
        {
            try
            {
                return _DbConnection.Table<User>().Where(x => x.Email == email).First();
            }
            catch (System.InvalidOperationException ioe)
            {
                System.Console.WriteLine(ioe.StackTrace);
                return null;
            }
        }

        public User GetUserById(int userId)
        {
            try
            {
                return _DbConnection.Table<User>().First(x => x.ID == userId);
            }
            catch (System.InvalidOperationException ioe)
            {
                System.Console.WriteLine(ioe.StackTrace);
                return null;
            }
        }

        public UserUser GetContact(int lowerUserId, int upperUserId)
        {
            int lowerId = lowerUserId;
            int upperId = upperUserId;
            if (lowerUserId > upperUserId)
            {
                lowerId = upperUserId;
                upperId = lowerUserId;
            }

            try
            {
                var contacts = _DbConnection.Table<UserUser>().Where(x => x.PrimaryUserId == lowerId).ToList();
                return contacts.Find(x => x.ContactUserId == upperId);
            }
            catch (System.InvalidOperationException ioe)
            {
                System.Console.WriteLine(ioe.StackTrace);
                return null;
            }
        }


        private bool CheckGroupExists(string groupName)
        {
            var grp = _DbConnection.Table<Group>().FirstOrDefault(x => x.GroupName == groupName);

            return grp != null;
        }

        public void Dispose()
        {
            System.GC.SuppressFinalize(this);
        }

        public List<AudioRecord> GetUserAudioRecordings(int userId)
        {
            return _DbConnection.Table<AudioRecord>().Where(ar => ar.CreatorId == userId).ToList();
        }

        public Group GetGroupByName(string name)
        {
            return _DbConnection.Table<Group>().Where(x => x.GroupName == name).First();
        }

        public bool CreateGroupUser(GroupUser gu)
        {
            try
            {
                _DbConnection.Insert(gu);
                return true;
            }
            catch (System.InvalidOperationException ioe)
            {
                System.Console.WriteLine(ioe.StackTrace);
                return false;
            }
        }
    }
}