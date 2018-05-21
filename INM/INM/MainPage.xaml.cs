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
		User user;
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
			user = new User() { FirstName = "TestFirstName", LastName = "TesteLastName", ID = 1, Contacts = new List<User>(), Email = EmailEntry.Text, Groups = new List<Models.Group>(), PhoneNumber = "123-456-test", Username = "Tester's Labrotory" };
			if (EmailEntry.Text.ToLower() == "test" && PasswordEntry.Text == "123456")
			{
				Navigation.PushAsync(new HomePage(user));
			}
		}
	}
}
