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
    public partial class AddToGroupPage : ContentPage
    {
        User user;
        Group group = new Group();
        List<User> users = new List<User>();
        List<User> addedUsers = new List<User>();
        public AddToGroupPage(User user)
        {
            this.user = user;
            InitializeComponent();
            using (var sq = new Persistence.SQLiteDb())
            {
                user.Contacts.Clear();
                sq.GetContactList(user);
            }
            DisplayContacts();
        }

        private void DisplayContacts()
        {
            foreach (User cont in user.Contacts)
            {
                users.Add(cont);
                StackLayout sl = new StackLayout {Orientation=StackOrientation.Horizontal };
                Label l = new Label
                {
                    Text = $"{cont.FirstName} {cont.LastName}",
                    FontSize = 15,
                    Margin = new Thickness(0,3,0,0)
                };
                Button b = new Button
                {
                    Text = "Add",
                    FontSize = 9,
                    WidthRequest = 60,
                    HeightRequest = 30,
                    HorizontalOptions = LayoutOptions.End

                };
                b.Clicked += AddUserToList;
                sl.Children.Add(l);
                sl.Children.Add(b);
                DisplayContactsStackLayout.Children.Add(sl);
            }
        }

        public void AddUserToList(object sender, EventArgs args)
        {
            int index = DisplayContactsStackLayout.Children.IndexOf(((StackLayout)((Button)sender).Parent));
            addedUsers.Add(users[index]);
            ((StackLayout)((Button)sender).Parent).Children.Clear();
        }


        public void CreateGroupButton_Clicked(object sender, EventArgs args)
        {
            if (!string.IsNullOrWhiteSpace(GroupNameEntry.Text))
            {
                string name = GroupNameEntry.Text;
                group.GroupName = name;
                group.LeadUserId = user.ID;
                using (var db = new Persistence.SQLiteDb())
                {
                    db.CreateGroup(group);
                    group = db.GetGroupByName(name);
                    GroupUser gu = new GroupUser();
                    gu.GroupId = group.ID;
                    gu.IsUserGroupLeader = true;
                    gu.UserId = user.ID;
                    db.CreateGroupUser(gu);
                    foreach (User groupUser in addedUsers)
                    {
                        GroupUser groupu = new GroupUser
                        {
                            GroupId = group.ID,
                            IsUserGroupLeader = true,
                            UserId = groupUser.ID
                        };
                        db.CreateGroupUser(groupu);
                    }
                };
                DisplayAlert("","Group Created", "Okay");
                Navigation.PopAsync();
                Navigation.PushAsync(new GroupsPage(user));
            }
            else
            {
                DisplayAlert("","You need to name the group","Okay");
            }
            
        }

        private void GroupNameEntry_Focused(object sender, FocusEventArgs e)
        {
            GroupNameEntry.Text = "";
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
        private void ProfileToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ProfilePage(user));
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
