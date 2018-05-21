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
		public MainPage()
		{
			InitializeComponent();
		}

		private void CreateAccountBtn_Clicked(object sender, EventArgs e)
		{
			this.Navigation.PushAsync(new CreateAccountPage());
		}

		private void LoginBtn_Clicked(object sender, EventArgs e)
		{
			DisplayAlert("Test", $"{EmailEntry.Text} - {PasswordEntry.Text}", "Cancel");
			Navigation.PushAsync(new HomePage());
		}
	}
}
