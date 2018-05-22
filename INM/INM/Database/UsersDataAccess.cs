using SQLite;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using INM.Models;
using static INM.MainPage;

namespace INM.Database
{
    public class UsersDataAccess
    {
        private SQLiteConnection Database;
        private static object CollisionLock = new object();
        public ObservableCollection<User> Users { get; set; }

        public UsersDataAccess()
        {
            Database = DependencyService.Get<IDatabaseConnection>().DbConnection();
            Database.CreateTable<User>();
            this.Users = new ObservableCollection<User>(Database.Table<User>());
            if (!Database.Table<User>().Any())
            {
                AddNewUser();
            }
        }

        private void AddNewUser()
        {
            Users.Add(new User
            {
                Username = "Default",
                Email = "empty",
                FirstName = "John",
                LastName = "Doe",
                
            });
        }

        public User Login(string username, string password)
        {
            lock (CollisionLock)
            {
                return Database.Table<User>().FirstOrDefault(user => user.Email == username);
            }
        }

        public User GetUser(int id)
        {
            lock (CollisionLock)
            {
                return Database.Table<User>().FirstOrDefault(user => user.ID == id);
            }
        }

        public int SaveUser(User user)
        {
            lock (CollisionLock)
            {
                if (user.ID != 0)
                {
                    Database.Update(user);
                    return user.ID;
                }
                else
                {
                    Database.Insert(user);
                    return user.ID;
                }
            }
        }

        public int DeleteCustomer(User user)
        {
            var id = user.ID;
            if (id != 0)
            {
                lock (CollisionLock)
                {
                    Database.Delete<User>(id);
                }
            }
            Users.Remove(user);
            return id;
        }
    }
}
