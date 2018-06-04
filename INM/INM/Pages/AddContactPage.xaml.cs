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
                            Contacts.Add(user);
                            Label l = new Label
                            {
                                Text = $"{user.FirstName} {user.LastName}",
                            };
                            ContactList.Children.Add(l);
                            Button b = new Button
                            {
                                Text = "ADD"
                            };
                            b.Clicked += B_Clicked;
                            ContactList.Children.Add(b);
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
            int index = ContactList.Children.IndexOf((Button)sender)-1;
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
                        DisplayAlert("", $"{records.ID} {records.PrimaryUserId} {records.ContactUserId}", "Ok");
                    }
                    else
                    {
                        addSuccessful = sq.CreateContact(Contacts[index].ID, user.ID);
                        sq.UpdateUser(user);
                    }
                }
                string retMsg = addSuccessful ? "User Added" : "Something went wrong";
                DisplayAlert("", retMsg, "Okay");
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

        private void SearchBarEntry_Unfocused(object sender, FocusEventArgs e)
        {
            SearchBarEntry.Text = "Search for People";

        }
    }
}

