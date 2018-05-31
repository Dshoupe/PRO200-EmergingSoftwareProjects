using INM.Pages;
using System;
using Xamarin.Forms;

namespace INM
{
				public partial class MainPage : ContentPage
				{
								public Models.User CurrentUser { get; set; }

								public MainPage()
								{
												InitializeComponent();
												//DependencyService.Get<Persistence.SQLiteDb>().GetConnection();						
								}

								private void CreateAccountBtn_Clicked(object sender, EventArgs e)
								{
												Navigation.PushAsync(new CreateAccountPage());
												PasswordEntry.Text = "";
								}

								private void LoginBtn_Clicked(object sender, EventArgs e)
								{
												using (var sq = new Persistence.SQLiteDb())
												{
																var authdUser = sq.FindUserByEmail(EmailEntry.Text);
																authdUser = authdUser ?? sq.FindUserByUsername(EmailEntry.Text);

																if (authdUser != null)
																{
																				if (authdUser.Password.Equals(PasswordEntry.Text))
																				{
																								Navigation.PushAsync(new HomePage(authdUser));
																				}
																				else
																				// wrong password
																				{
																								DisplayAlert("", "Username or password is invalid", "Okay");
																				}
																}
																else
																// user not found by email nor username
																{
																				DisplayAlert("", "Username or password is invalid", "Okay");
																}
												}

												PasswordEntry.Text = "";
								}

								/// <summary>
								/// Clears the Email and Password fields
								/// </summary>
								private void ClearFields()
								{
												EmailEntry.Text = "";
												PasswordEntry.Text = "";
								}
				}
}
