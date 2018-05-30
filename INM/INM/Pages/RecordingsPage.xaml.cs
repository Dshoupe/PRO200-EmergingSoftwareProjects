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
	public partial class RecordingsPage : ContentPage
	{
		User user;
		public RecordingsPage (User user)
		{
			InitializeComponent ();
			this.user = user;
			DisplayRecordings();
		}

		private void DisplayRecordings()
		{
			if(user.Recordings.Count == 0)
			{
				Label noRecordingsLabel = new Label
				{
					Text = "You have no records at this time",
					FontSize = 10.0,
					HorizontalTextAlignment = TextAlignment.Center
				};
				RecordingsStackLayout.Children.Add(noRecordingsLabel);
			}
			else
			{

			}
		}

		private void ContactsToolbarItem_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new ContactsPage(user));
		}

		private void HomeToolbarItem_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new HomePage(user));
		}

		private void GroupsToolbarItem_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new GroupsPage(user));
		}

		private void ProfileToolbarItem_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new ProfilePage(user));
		}

		private void SignOutToolbarItem_Clicked(object sender, EventArgs e)
		{
			Navigation.PopToRootAsync();
		}
	}
}