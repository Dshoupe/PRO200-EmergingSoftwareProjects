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
	public partial class GroupsPage : ContentPage
	{
		User user;
		public GroupsPage (User user)
		{
			InitializeComponent ();
			this.user = user;
		}

		private void DisplayGroups()
		{
			if(user.Groups.Count == 0)
			{
				Label noGroupsLabel = new Label
				{
					Text = "You have currently have no groups",
					FontSize = 10.0
				};
				GroupsStackLayout.Children.Add(noGroupsLabel);
			}
			else
			{
				//Get the user's groups 
			}
		}

		private void HomeToolbarItem_Clicked(object sender, EventArgs e)
		{

		}

		private void ContactsToolbarItem_Clicked(object sender, EventArgs e)
		{

		}

		private void RecordingsToolbarItem_Clicked(object sender, EventArgs e)
		{

		}

		private void ProfileToolbarItem_Clicked(object sender, EventArgs e)
		{

		}

		private void SignOutToolbarItem_Clicked(object sender, EventArgs e)
		{

		}
	}
}