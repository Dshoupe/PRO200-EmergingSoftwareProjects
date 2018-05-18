using innaMinute.Droid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace innaMinute.Pages
{
				[XamlCompilation(XamlCompilationOptions.Compile)]
				public partial class CreateAccountPage : ContentPage
				{
								public CreateAccountPage()
								{
												InitializeComponent();
								}

								private void CreateAccountBtn_Clicked(object sender, EventArgs e)
								{
												User user = new User() { FirstName = FNameEntry.Text, LastName = LNameEntry.Text, ID = 1, Contacts = new List<User>(), Email = EmailEntry.Text, Groups = new List<Droid.Models.Group>(), PhoneNumber = PhoneEntry.Text, Username = UsernameEntry.Text };
												DisplayAlert("User Created", user.ToString(), "Ok");
								}

								private void PasswordEntry_TextChanged(object sender, TextChangedEventArgs e)
								{
												//Regex regex = new Regex("[^0-9]+");
												//e.Handled = regex.IsMatch(e.Text);
								}
				}
}