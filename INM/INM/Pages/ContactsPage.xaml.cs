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
    public partial class ContactsPage : ContentPage
    {
        User user;
        public ContactsPage(User user)
        {
            InitializeComponent();
            this.user = user;
            DisplayContacts();
        }

        private void DisplayContacts()
        {
            if (user.Contacts.Count == 0)
            {
                Label noContactsLabel = new Label
                {
                    HorizontalTextAlignment = TextAlignment.Center,
                    FontSize = 10,
                    Text = "No Contacts"
                };
                ContactpageStackLayout.Children.Insert(1, noContactsLabel);
            }
            else
            {
                foreach (User u in user.Contacts)
                {
                    Label l = new Label
                    {
                        Text = $"{u.FirstName} {u.LastName}"
                    };
                    ContactpageStackLayout.Children.Insert(1, l);
                }
            }
        }

        private void HomeToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage(user));
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

        private void SignOutToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PopToRootAsync();
        }

        private void AddNewContactButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddContactPage(user));
        }
    }
}