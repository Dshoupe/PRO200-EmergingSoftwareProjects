using Android.Media;
using System;
using INM.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Timers;
using System.Speech.Recognition;
using System.Text;
using System.Diagnostics;
using Plugin.SpeechRecognition;

namespace INM.Pages
{
				[XamlCompilation(XamlCompilationOptions.Compile)]
				public partial class HomePage : ContentPage
				{
								User user;
								bool recordClicked = false;
								Time time = new Time();
								MediaRecorder recorder = new MediaRecorder();
								string test = "";
								IDisposable listener = null;

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
																recorder.SetOutputFile(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + $"/{DateTime.Now.ToString("MM-dd-yyyy-HH;mm;ss")}.mp3");
																recorder.Prepare();
																recorder.Start();
																RecordButton.Source = "stopbuttonimage.png";
																recordClicked = true;
																listener = CrossSpeechRecognition.Current.ContinuousDictation().Subscribe(phrase =>
																{
																				test += phrase;
																});
																Device.StartTimer(new TimeSpan(0, 0, 1), () => { time.Seconds++; timeLabel.Text = time.ToString(); return recordClicked; });
												}
												else
												{
																testLabel.Text = test;
																StopRecording();
												}
								}

								private void StopRecording()
								{
												if (recorder != null)
												{
																RecordButton.Source = "recordbutton.png";
																recorder.Stop();
																recorder.Release();
																recorder.Dispose();
																listener.Dispose();
																recorder = null;
																recordClicked = false;
																time.Reset();
												}
								}

								//private void PlayTestBtn_Clicked(object sender, EventArgs e)
								//{
								//				if (!recordClicked)
								//				{
								//								MediaPlayer mp = new MediaPlayer();
								//								if (!playClicked)
								//								{
								//												mp.Reset();
								//												mp.SetDataSource(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/test.mp3");
								//												mp.Prepare();
								//												mp.Start();
								//												playClicked = true;
								//								}
								//								else
								//								{
								//												mp.Stop();
								//												mp.Release();
								//												playClicked = false;
								//								}
								//				}
								//				else
								//				{
								//								DisplayAlert("Error", "Cannot play while recording", "Ok");
								//				}
								//}

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