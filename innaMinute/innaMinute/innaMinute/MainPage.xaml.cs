using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace innaMinute
{
				public partial class MainPage : ContentPage
				{
								public MainPage()
								{
												InitializeComponent();
								}

								private void CreateAccountBtn_Clicked(object sender, EventArgs e)
								{

								}

								private void LoginBtn_Clicked(object sender, EventArgs e)
								{
												DisplayAlert("Test", $"{EmailEntry.Text} - {PasswordEntry.Text}", "Cancel");
								}
				}
}
