
using Android.Media;
using System;
using INM.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace INM.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
		User user;
		bool recordClicked = false;
		Time time = new Time();
		MediaRecorder recorder = new MediaRecorder();
		private string audioFilePath;

		public HomePage(User user)
		{
			InitializeComponent();
			this.user = user;
		}

		private void RecordButton_Tapped(object sender, EventArgs e)
		{
			if (!recordClicked)
			{
				if (recorder == null)
				{
					recorder = new MediaRecorder();
				}
				else
				{
					recorder.Reset();
				}
				recorder.SetAudioSource(AudioSource.Mic);
				recorder.SetOutputFormat(OutputFormat.Mpeg4);
				recorder.SetAudioEncoder(AudioEncoder.AmrNb);
				audioFilePath = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + $"/{DateTime.Now.ToString("MM-dd-yyyy-HH;mm;ss")}.mp3";
				recorder.SetOutputFile(audioFilePath);
				recorder.Prepare();
				recorder.Start();
				RecordButton.Source = "stopbuttonimage.png";
				recordClicked = true;
				Device.StartTimer(new TimeSpan(0, 0, 1), () => { time.Seconds++; timeLabel.Text = time.ToString(); return recordClicked; });
			}
			else
			{
				StopRecording();
			}
		}

		private void StopRecording()
		{
			if (recorder != null && recordClicked)
			{
				RecordButton.Source = "recordbutton.png";
				recorder.Stop();
				recorder.Release();
				recorder.Dispose();
				recorder = null;
				recordClicked = false;
				time.Reset();

				

                

				using (var db = new Persistence.SQLiteDb())
				{
                    byte[] audioBytes = System.IO.File.ReadAllBytes(audioFilePath);
                    Models.AudioRecord ar = new Models.AudioRecord()
                    {
                        AudioClip = audioBytes,
                        Title = DateTime.Now.ToString("MM-dd-yyyy-HH;mm;ss"),
                        ID = db.GetRecordings().Count+1,
                        CreatorId = user.ID
                    };
                    user.Recordings.Add(ar);
                    db.UpdateUser(user);
					if (db.CreateRecording(ar))
					{
						Android.Util.Log.WriteLine(Android.Util.LogPriority.Info, "Audio Save", $"Audio saved for user {user.ID}");
					}
					else
					{
						DisplayAlert("", "Did not save audio", "OK");
					}
				}
			}
		}

		private void ContactsToolbarItem_Clicked(object sender, EventArgs e)
		{
			StopRecording();
			Navigation.PushAsync(new ContactsPage(user));
		}

		private void RecordingsToolbarItem_Clicked(object sender, EventArgs e)
		{
			StopRecording();
			Navigation.PushAsync(new RecordingsPage(user));
		}

		private void GroupsToolbarItem_Clicked(object sender, EventArgs e)
		{
			StopRecording();
			Navigation.PushAsync(new GroupsPage(user));
		}

		private void ProfileToolbarItem_Clicked(object sender, EventArgs e)
		{
			StopRecording();
			Navigation.PushAsync(new ProfilePage(user));
		}

		private void SignOutToolbarItem_Clicked(object sender, EventArgs e)
		{
			StopRecording();
			Navigation.PopToRootAsync();
		}
	}
}