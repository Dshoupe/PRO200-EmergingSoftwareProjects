using Android.Media;
using Java.IO;
using Java.Lang;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace INM.Pages
{
				[XamlCompilation(XamlCompilationOptions.Compile)]
				public partial class HomePage : ContentPage
				{
								MediaRecorder recorder = null;
								bool recordClicked = false;
								bool playClicked = false;
								public HomePage()
								{
												InitializeComponent();
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
																recorder.SetOutputFile(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/test.mp3");
																recorder.Prepare();
																recorder.Start();
																recordClicked = true;
												}
												else
												{
																recorder.Stop();
																recorder.Release();
																recorder = null;
																recordClicked = false;
												}
								}

								private void PlayTestBtn_Clicked(object sender, EventArgs e)
								{
												if(!recordClicked)
												{
																MediaPlayer mp = new MediaPlayer();
																if (!playClicked)
																{
																				mp.Reset();
																				mp.SetDataSource(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/test.mp3");
																				mp.Prepare();
																				mp.Start();
																				playClicked = true;
																}
																else
																{
																				mp.Stop();
																				mp.Release();
																				playClicked = false;
																}
												}
												else
												{
																DisplayAlert("Error", "Cannot play while recording", "Ok");
												}
								}
				}
}