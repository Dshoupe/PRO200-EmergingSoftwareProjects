using System.Collections.Generic;
using INM.Models;
using System.Linq;
using System.Security.Cryptography;

//[assembly: Xamarin.Forms.Dependency(typeof(INM.Persistence.SQLiteDb))]
namespace INM.Persistence
{
	public class SQLiteDb : ISQLiteDb, System.IDisposable
	{
		public static SQLite.SQLiteConnection _DbConnection { get; set; } = null;

		public SQLiteDb()
		{
			string applicationFolderPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "persistence");
			if (!System.IO.Directory.Exists(applicationFolderPath))
			{
				System.IO.Directory.CreateDirectory(applicationFolderPath);			
			}
			string databaseFilePath = System.IO.Path.Combine(applicationFolderPath, "inmDB.sqlite");
			_DbConnection = new SQLite.SQLiteConnection(databaseFilePath);

			// create db tables
			_DbConnection.CreateTable<User>(); // users
			_DbConnection.CreateTable<UserUser>(); // contacts
			_DbConnection.CreateTable<Group>(); // groups
			_DbConnection.CreateTable<GroupUser>(); // groups with users
			_DbConnection.CreateTable<GroupAudioRecord>(); // groups with audio records
			_DbConnection.CreateTable<Transcript>(); // audio text
			_DbConnection.CreateTable<AudioRecord>(); // audio data
		}

		public bool CreateContact(User fromUser, User toUser)
		{
			throw new System.NotImplementedException();
		}

		public bool CreateGroup(Group newGroup)
		{
			throw new System.NotImplementedException();
		}

		public bool CreateRecording(AudioRecord newAudio)
		{
			throw new System.NotImplementedException();
		}

		public bool CreateUser(User newUser)
		{
			// check if user exists
			var userByEmail = FindUserByEmail(newUser.Email);
			var userByUsername = FindUserByUsername(newUser.Username);

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

		public void Dispose()
		{
			System.GC.SuppressFinalize(this);			
		}		

		public List<Group> GetGroups()
		{
			throw new System.NotImplementedException();
		}

		public List<User> GetUsers()
		{
			return _DbConnection.Table<User>().Select(u => u).ToList();
		}

		public bool UpdateContact()
		{
			throw new System.NotImplementedException();
		}

		public bool UpdateGroup()
		{
			throw new System.NotImplementedException();
		}

		public bool UpdateRecording()
		{
			throw new System.NotImplementedException();
		}

		public bool UpdateUser()
		{
            throw new System.NotImplementedException();
        }


		public User FindUserByUsername(string userName)
		{
			try
			{
				return _DbConnection.Table<User>().Where(x => x.Username == userName).First();
			}
			catch (System.InvalidOperationException ioe)
			{
				return null;
			}
		}

		public User FindUserByEmail(string email)
		{
			try
			{
				return _DbConnection.Table<User>().Where(x => x.Email == email).First();			
			}
			catch (System.InvalidOperationException ioe)
			{
				return null;
			}		
		}

		public void GetConnection()
		{
			throw new System.NotImplementedException();
		}
	}
}
