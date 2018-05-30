using INM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace INM.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{
		User user;
		public ProfilePage (User user)
		{
			InitializeComponent ();
			this.user = user;
			ProfilePageGrid.BindingContext = user;
		}

		private void ContactsToolbarItem_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new ContactsPage(user));
		}

		private void RecordingsToolbarItem_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new RecordingsPage(user));
		}

		private void GroupsToolbarItem_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new GroupsPage(user));
		}

		private void HomeToolbarItem_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new HomePage(user));
		}

		private void SignOutToolbarItem_Clicked(object sender, EventArgs e)
		{
			Navigation.PopToRootAsync();
		}
	}
}