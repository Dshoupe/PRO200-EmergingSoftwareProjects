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
            ContactsPane.Children.Clear();

            using (var sq = new Persistence.SQLiteDb())
			{
                user.Contacts.Clear();
				sq.GetContactList(user);
			}

			if (user.Contacts.Count == 0)
			{
				Label noContactsLabel = new Label
				{
					HorizontalTextAlignment = TextAlignment.Center,
					FontSize = 10,
					Text = "No Contacts"
				};
				ContactsPane.Children.Add(noContactsLabel);
			}
			else
			{
				foreach (User u in user.Contacts)
				{
                    StackLayout sl = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal
                    };
                    Label l = new Label
					{
						Text = $"{u.FirstName} {u.LastName}"
					};
					Image i = new Image() { WidthRequest = 20, HeightRequest = 20 };
					i.Source = "redX.png";
					i.ClassId = $"{user.ID}-{u.ID}";
					TapGestureRecognizer g2 = new TapGestureRecognizer();
					g2.Tapped += DeleteContact;
					i.Margin = 0;
					i.GestureRecognizers.Add(g2);
					sl.Children.Add(l);
					sl.Children.Add(i);
					ContactpageStackLayout.Children.Insert(1, sl);
				}
			}
		}

		private void DeleteContact(object sender, EventArgs e)
		{
            bool hasDeleted = false;
			using (var sq = new Persistence.SQLiteDb())
			{
				string[] holder = ((Image)sender).ClassId.Split('-');
				hasDeleted = sq.DeleteContact(int.Parse(holder[0]), int.Parse(holder[1]));
			}
            string retVal = hasDeleted ? "Contact Deleted" : "Something went wrong";
            DisplayAlert("",retVal, "Okay");
            Navigation.PopAsync();
            Navigation.PushAsync(new ContactsPage(user));
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