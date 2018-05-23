using SQLite;
using LocalDataAccess.Droid;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnection_Android))]

namespace LocalDataAccess.Droid
{
    public class DatabaseConnection_Android
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "INMdb.db3";
            var path = Path.Combine("C:\\Users\\KStringer\\Documents", dbName);
            return new SQLiteConnection(path);
        }
    }
}