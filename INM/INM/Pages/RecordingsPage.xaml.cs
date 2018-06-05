using Android.Media;
using INM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Java.IO;

namespace INM.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RecordingsPage : ContentPage
	{
		User user;
		private List<Models.AudioRecord> records;
		public RecordingsPage(User user)
		{
			InitializeComponent();
			this.user = user;
			DisplayRecordings();
		}

		private void DisplayRecordings()
		{
			RecordingsStackLayout.Children.Clear();
			
			//string[] recordings = Directory.GetFiles(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "*.mp3");
				
			using (var db = new Persistence.SQLiteDb())
			{
				records = db.GetUserAudioRecordings(user.ID);
			}

			if (records.Count == 0)
			{
				RecordingsStackLayout.Children.Clear();
				Frame f = new Frame
				{
					BorderColor = Color.Silver
				};
				Label l = new Label
				{
					Text = "There are no Recordings yet!",
					FontSize = 20
				};
				f.Content = l;
				RecordingsStackLayout.Children.Add(f);
			}
			else
			{
				ScrollView sv = new ScrollView
				{
					Content = CreateRecordingsStack()
				};

				RecordingsStackLayout.Children.Add(sv);
			}
		}

		private StackLayout CreateRecordingsStack()
		{
			StackLayout top = new StackLayout();
			foreach (var recording in records)
			{
				//string[] splitPath = recording.Split('/');
				//string path = splitPath[4].Substring(0, splitPath[4].Length - 4);

				StackLayout sl = new StackLayout
				{
					Orientation = StackOrientation.Horizontal
				};

				Frame f = new Frame
				{
					BorderColor = Color.Silver
				};

				Label l = new Label
				{
					Text = recording.Title,
					FontSize = 10,
					Margin = new Thickness(0, 0, 10, 0),
					BindingContext = recording
				};
				TapGestureRecognizer g = new TapGestureRecognizer();
				g.Tapped += PlayRecording;
				l.GestureRecognizers.Add(g);

				Xamarin.Forms.Image i = new Xamarin.Forms.Image() { WidthRequest = 20, HeightRequest = 20 };
				i.Source = "redX.png";
				i.ClassId = recording.Title;
				TapGestureRecognizer g2 = new TapGestureRecognizer();
				g2.Tapped += DeleteRecording;
				i.Margin = 0;
				i.GestureRecognizers.Add(g2);

				Label l2 = new Label();
				TapGestureRecognizer g3 = new TapGestureRecognizer();
				l2.GestureRecognizers.Add(g3);
				l2.Text = "Edit Recording";
				l2.FontSize = 10;
				l2.ClassId = recording.Title;

				g3.Tapped += ChangeRecordingName;
				sl.Children.Add(l);
				sl.Children.Add(l2);
				sl.Children.Add(i);
				f.Content = sl;
				top.Children.Add(f);
			}
			return top;
		}


		private void ChangeRecordingName(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(EditEntry.Text.Trim()))
			{
				DisplayAlert("Error", "Invalid Name", "Ok");
			}
			else
			{
				try
				{
					Label l = (Label)sender;
					System.IO.File.Move($"{Android.OS.Environment.ExternalStorageDirectory.AbsolutePath}/{l.ClassId}.mp3", $"{Android.OS.Environment.ExternalStorageDirectory.AbsolutePath}/{EditEntry.Text}.mp3");
					DisplayRecordings();
				}
				catch (Exception)
				{
					DisplayAlert("Error", "Another recording already has that name", "Ok");
				}
			}
		}

		private void DeleteRecording(object sender, EventArgs e)
		{
			//Xamarin.Forms.Image i = (Xamarin.Forms.Image)sender;
			//System.IO.File.Delete($"{Android.OS.Environment.ExternalStorageDirectory.AbsolutePath}/{i.ClassId}.mp3");
			DisplayRecordings();
		}

		MediaPlayer mp = new MediaPlayer();
		private void PlayRecording(object sender, EventArgs e)
		{
			Label l = (Label)sender;
			Java.IO.File tempFile = Java.IO.File.CreateTempFile($"{l.Text}", ".mp3");
			Models.AudioRecord ar = (Models.AudioRecord)l.BindingContext;

			if (!mp.IsPlaying)
			{
				
				FileOutputStream fos = new Java.IO.FileOutputStream(tempFile);
				fos.Write(ar.AudioClip);
				fos.Close();

				mp.Reset();
				//string path = $"{Android.OS.Environment.ExternalStorageDirectory.AbsolutePath}/{l.Text}.mp3";
				Java.IO.FileInputStream fis = new Java.IO.FileInputStream(tempFile);
				mp.SetDataSource(fis.FD);
				mp.Prepare();
				mp.Start();
			}
			else
			{
				mp.Stop();
				mp.Release();
				tempFile.Delete();				
			}

		}

        private void EditEntry_Focused(object sender, FocusEventArgs e)
        {
            EditEntry.Text = "";
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