using INM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace INM.Pages
{
    public partial class AddContactPage : ContentPage
    {
        public User user;
        public List<User> Contacts { get; set; }


        public AddContactPage(User user)
        {
            InitializeComponent();
            this.user = user;
            Contacts = new List<User>();
        }

        private void SearchButton_Clicked(object sender, EventArgs e)
        {
            ContactList.Children.Clear();
            Contacts.Clear();
            using (var sq = new Persistence.SQLiteDb())
            {
                var users = sq.GetUsers();
                var searchedUsers = users.Where(x => x.Email.Contains(SearchBarEntry.Text)).ToList();

                if (searchedUsers.Count > 0)
                {
                    foreach (var user in searchedUsers)
                    {
                        if (this.user.Contacts.Where(x => x.ID == user.ID).ToList().Count == 0 && this.user.ID != user.ID)
                        {
                            StackLayout sl = new StackLayout {Orientation = StackOrientation.Horizontal};
                            Contacts.Add(user);
                            Label l = new Label
                            {
                                Text = $"{user.FirstName} {user.LastName}",
                                FontSize = 15,
                                Margin = new Thickness(0, 3, 0, 0)
                            };
                            sl.Children.Add(l);
                            Button b = new Button
                            {
                                Text = "ADD",
                                FontSize = 9,
                                WidthRequest = 60,
                                HeightRequest = 30,
                                HorizontalOptions = LayoutOptions.End
                            };
                            b.Clicked += B_Clicked;
                            sl.Children.Add(b);
                            ContactList.Children.Add(sl);
                        }
                    }
                    //if no users are added. this will hit when only contacts already in the contact list and the user are found.
                    if (ContactList.Children.Count == 0)
                    {
                        DisplayAlert("", "No Users Found", "Okay");
                    }
                }
                else
                // user not found by email nor username
                {
                    DisplayAlert("", "No Users Found", "Okay");
                }
            }
        }

        private void B_Clicked(object sender, EventArgs e)
        {
            int index = ContactList.Children.IndexOf(((StackLayout)((Button)sender).Parent));
            if (!user.Contacts.Contains(Contacts[index]))
            {
                bool addSuccessful = false;
                this.user.Contacts.Add(Contacts[index]);
                using (var sq = new Persistence.SQLiteDb())
                {
                    if (user.ID < Contacts[index].ID)
                    {
                        addSuccessful = sq.CreateContact(user.ID, Contacts[index].ID);
                        sq.UpdateUser(user);
                        var records = sq.GetContact(user.ID, Contacts[index].ID);
                        
                    }
                    else
                    {
                        addSuccessful = sq.CreateContact(Contacts[index].ID, user.ID);
                        sq.UpdateUser(user);
                    }
                }
                string retMsg = addSuccessful ? "User Added" : "Something went wrong";
                DisplayAlert("", retMsg, "Okay");
                ((StackLayout)((Button)sender).Parent).Children.Clear();
            }
            else
            {
                DisplayAlert("","That user is already in your contact's list", "Okay");
            }
        }

        private void SearchBarEntry_Focused(object sender, FocusEventArgs e)
        {
            SearchBarEntry.Text = "";
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

        private void ProfileToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ProfilePage(user));
        }

        private void SignOutToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PopToRootAsync();
        }

        private void DoneButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
            Navigation.PushAsync(new ContactsPage(user));
        }
    }
}

