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
			InitializeComponent();
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
                GroupsStackLayout.Children.Add(noGroupsLabel);
			}
			else
			{
                foreach (Group g in user.Groups)
                {
                    StackLayout sl = new StackLayout{ Orientation=StackOrientation.Horizontal};
                    Label l = new Label
                    {
                        Text = g.GroupName
                    };
                    sl.Children.Add(l);
                    Image i = new Image() { WidthRequest = 20, HeightRequest = 20 };
                    i.Source = "redX.png";
                    i.ClassId = $"{g.ID}";
                    TapGestureRecognizer g2 = new TapGestureRecognizer();
                    g2.Tapped += DeleteGroupButton_Clicked;
                    i.Margin = 0;
                    i.GestureRecognizers.Add(g2);
                    sl.Children.Add(l);
                    sl.Children.Add(i);
                    sl.Children.Add(i);
                    GroupsStackLayout.Children.Add(sl);
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
        //private void groupButton_Clicked(object sender, EventArgs e)
        //{
        //    using (var sq = new Persistence.SQLiteDb())
        //    {
        //        Group tempGroup = new Group
        //        {
        //            GroupName = "Test Group",
        //            LeadUserId = user.ID
        //        };
        //        sq.CreateGroup(tempGroup);
        //        user.Groups = sq.GetUserGroups(user.ID);
        //    }
        //}
    
		private void CreateGroupButton_Clicked(object sender, EventArgs e)
		{
			// create a new 'AddToGroupPage'
			Navigation.PushAsync(new AddToGroupPage(user));
		}
        private void DeleteGroupButton_Clicked(object sender, EventArgs e)
        {
            Label l = ((Label)((StackLayout)((Image)sender).Parent).Children[0]);
            using (var db = new Persistence.SQLiteDb())
            {
                Group group = db.GetGroupByName(l.Text);
                db.DeleteGroup(group.ID);
                DisplayAlert("","Group Deleted","Okay");
                Navigation.PopAsync();
                Navigation.PushAsync(new GroupsPage(user));
            };
        }
    }
}