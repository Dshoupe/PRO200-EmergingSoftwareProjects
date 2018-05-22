using INM.Database;
using INM.Models;
using INM.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace INM
{
    public partial class MainPage : ContentPage
    {
        public User CurrentUser { get; set; }
        public UsersDataAccess UDA { get; set; } = new UsersDataAccess();
        public MainPage()
        {
            InitializeComponent();
        }

        private void CreateAccountBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateAccountPage());
        }

        private void LoginBtn_Clicked(object sender, EventArgs e)
        {
            //DisplayAlert("Test", $"{EmailEntry.Text} - {PasswordEntry.Text}", "Cancel");
            if (EmailEntry.Text.ToLower() == "test" && PasswordEntry.Text == "123456")
            {
                Navigation.PushAsync(new HomePage(new User()));
            }
        }
        public interface IDatabaseConnection
        {
            SQLite.SQLiteConnection DbConnection();
        }
    }
}
