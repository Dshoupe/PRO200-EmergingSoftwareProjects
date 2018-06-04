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
            DisplayGroups();
		}

		private void DisplayGroups()
		{
            using (var sq = new Persistence.SQLiteDb())
            {
                user.Groups = sq.GetUserGroups(user.ID);
            }
            if (user.Groups.Count == 0)
			{
				Label noGroupsLabel = new Label
				{
					Text = "You have currently have no groups",
					FontSize = 10.0
				};
				groupPane.Children.Add(noGroupsLabel);
			}
			else
			{
                foreach (Group g in user.Groups)
                {
                    Label l = new Label
                    {
                        Text = g.GroupName
                    };
                    groupPane.Children.Add(l);
                }
            }
		}

		private void HomeToolbarItem_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new HomePage(user));
		}

		private void ContactsToolbarItem_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new ContactsPage(user));
		}

		private void RecordingsToolbarItem_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new RecordingsPage(user));
		}

		private void ProfileToolbarItem_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new ProfilePage(user));
		}

		private void SignOutToolbarItem_Clicked(object sender, EventArgs e)
		{
			Navigation.PopToRootAsync();
		}

        private void groupButton_Clicked(object sender, EventArgs e)
        {
            using (var sq = new Persistence.SQLiteDb())
            {
                Group tempGroup = new Group
                {
                    GroupName = "Test Group",
                    LeadUserId = user.ID
                };
                sq.CreateGroup(tempGroup);
                user.Groups = sq.GetUserGroups(user.ID);
            }
        }
    }
}