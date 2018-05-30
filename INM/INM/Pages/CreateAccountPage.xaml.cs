using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using System;

namespace INM.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreateAccountPage : ContentPage
	{
		public CreateAccountPage()
		{
			InitializeComponent();			
		}

		/// <summary>
		/// Called when the page is rendered
		/// </summary>
		protected override void OnAppearing()
		{
			base.OnAppearing();
		}

		/// <summary>
		/// Creates a new user on Create Account button click
		/// </summary>		
		void CreateAccountBtn_Clicked(object sender, System.EventArgs e)
		{
			// do data field validation here? or can we automagically do it somehow via bindings/something else
			// since the User class has MaxLength constraints, 
			//		will it raise exception if we try to create a user with a field longer than the maxlength?

			// if the password can be confirmed matching
			if (PasswordEntry.Text == null || CPasswordEntry.Text == null)
			{
				DisplayAlert("User Creation Error", "You must supply matching passwords", "Okay");
			}
			else
			{			
				if (PasswordEntry.Text.Equals(CPasswordEntry.Text))
				{
					var user = CreateNewUser();

					// create db class instance
					var db = new Persistence.SQLiteDb();
					
					try
					{
						// try to add user to db
						if (db.CreateUser(user))
						{
							// go to home page
							Navigation.PushAsync(new HomePage(user));
							// pop this page off the nav stack
							Navigation.RemovePage(this);
						}
					}
					catch (System.ArgumentException ae)
					{
						// if a user already exists with the given email
						if (ae.ParamName == "Models.User.Email")
						{             
							// complain to user and re-direct back
							this.EmailEntry.TextColor = System.Drawing.Color.Red;
							DisplayAlert("User Creation Error", "A user already exists with that Email", "Okay");
						}
						// if a user already exists with the given username
						else if (ae.ParamName == "Models.User.UserName")
						{
							// complain to user and re-direct back
							this.UsernameEntry.TextColor = System.Drawing.Color.Red;
							DisplayAlert("User Creation Error", "A user already exists with that User Name", "Okay");
						}
					}				
				}
				else
				{
					// complain to user about password confirmation
					DisplayAlert("User Creation Error", "You must supply matching passwords", "Okay");
				}
			}
		}

		private Models.User CreateNewUser()
		{
			// create new user with the given data
			return new Models.User
			{
				Username = UsernameEntry.Text,
				Email = EmailEntry.Text,
				FirstName = FNameEntry.Text,
				LastName = LNameEntry.Text,
				PhoneNumber = PhoneEntry.Text,
				Password = PasswordEntry.Text
			};
		}

		private void PasswordEntry_TextChanged(object sender, TextChangedEventArgs e)
		{
			//Regex regex = new Regex("[^0-9]+");
			//e.Handled = regex.IsMatch(e.Text);
		}
	}
}