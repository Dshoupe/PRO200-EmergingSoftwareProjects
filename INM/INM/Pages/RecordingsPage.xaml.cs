using Android.Media;
using INM.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace INM.Pages
{
				[XamlCompilation(XamlCompilationOptions.Compile)]
				public partial class RecordingsPage : ContentPage
				{
								User user;
								public RecordingsPage(User user)
								{
												InitializeComponent();
												this.user = user;
												DisplayRecordings();
								}

								private void DisplayRecordings()
								{
												RecordingsStackLayout.Children.Clear();
												//if (recordings.Length == 0)
												//{
												//				Label noRecordingsLabel = new Label
												//				{
												//								Text = "You have no records at this time",
												//								FontSize = 10.0,
												//								HorizontalTextAlignment = TextAlignment.Center
												//				};
												//				RecordingsStackLayout.Children.Add(noRecordingsLabel);
												//}
												//else
												//{
												try
												{
																string[] recordings = Directory.GetFiles(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "*.mp3");
																ScrollView sv = new ScrollView();
																StackLayout top = new StackLayout();
																foreach (var recording in recordings)
																{
																				string[] splitPath = recording.Split('/');
																				string path = splitPath[4].Substring(0, splitPath[4].Length - 4);

																				StackLayout sl = new StackLayout();
																				sl.Orientation = StackOrientation.Horizontal;

																				Frame f = new Frame();
																				f.BorderColor = Color.Silver;

																				Label l = new Label();
																				l.Text = path;
																				l.FontSize = 10;
																				l.Margin = new Thickness(0, 0, 10, 0);
																				TapGestureRecognizer g = new TapGestureRecognizer();
																				g.Tapped += G_Tapped;
																				l.GestureRecognizers.Add(g);

																				Xamarin.Forms.Image i = new Xamarin.Forms.Image() { WidthRequest = 20, HeightRequest = 20 };
																				i.Source = "redX.png";
																				i.ClassId = path;
																				TapGestureRecognizer g2 = new TapGestureRecognizer();
																				g2.Tapped += G2_Tapped;
																				i.Margin = 0;
																				i.GestureRecognizers.Add(g2);

																				sl.Children.Add(l);
																				sl.Children.Add(i);
																				f.Content = sl;
																				top.Children.Add(f);
																}
																sv.Content = top;
																RecordingsStackLayout.Children.Add(sv);
												}
												catch (Exception)
												{
																Frame f = new Frame();
																f.BorderColor = Color.Silver;
																Label l = new Label();
																l.Text = "There are no Recordings yet!";
																l.FontSize = 10;
																f.Content = l;
																RecordingsStackLayout.Children.Add(f);
												}
												//}
								}

								private void G2_Tapped(object sender, EventArgs e)
								{
												Xamarin.Forms.Image i = (Xamarin.Forms.Image)sender;
												File.Delete($"{Android.OS.Environment.ExternalStorageDirectory.AbsolutePath}/{i.ClassId}.mp3");
												DisplayRecordings();
								}

								MediaPlayer mp = new MediaPlayer();
								private void G_Tapped(object sender, EventArgs e)
								{
												Label l = (Label)sender;
												if (!mp.IsPlaying)
												{
																mp.Reset();
																string path = $"{Android.OS.Environment.ExternalStorageDirectory.AbsolutePath}/{l.Text}.mp3";
																mp.SetDataSource(path);
																mp.Prepare();
																mp.Start();
												}
												else
												{
																mp.Stop();
																mp.Release();
												}
								}

								private void ContactsToolbarItem_Clicked(object sender, EventArgs e)
								{
												Navigation.PushAsync(new ContactsPage(user));
								}

								private void HomeToolbarItem_Clicked(object sender, EventArgs e)
								{
												Navigation.PushAsync(new HomePage(user));
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
				}
}