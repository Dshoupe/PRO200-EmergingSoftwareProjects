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
            using (var db = new Persistence.SQLiteDb())
            {
                user.Recordings = db.GetUserAudioRecordings(user.ID);
                user.Groups = db.GetUserGroups(user.ID);
            }

            Picker p = new Picker
            {
                WidthRequest = 50,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            foreach (Group g in user.Groups)
            {
                p.Items.Add(g.GroupName);
            }
            if (p.Items.Count() == 0)
            {
                p.Items.Add("You are not currently in any groups");
            }
            p.SelectedIndex = 0;

            RecordingsStackLayout.Children.Add(p);
            DisplayRecordings();
        }

        private void DisplayRecordings()
        {
            RecordingPane.Children.Clear();

            //string[] recordings = Directory.GetFiles(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "*.mp3");

            using (var db = new Persistence.SQLiteDb())
            {
                records = db.GetUserAudioRecordings(user.ID);
            }

            if (records.Count == 0)
            {
                RecordingPane.Children.Clear();
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
                RecordingPane.Children.Add(f);
            }
            else
            {

                CreateRecordingsStack();
            }
                CreateGroupRecordingsStack();
        }

        private void CreateRecordingsStack()
        {
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

                

                sl.Children.Add(l);
                sl.Children.Add(l2);
                sl.Children.Add(i);
                Button b = new Button { Text = "Share" };
                b.Clicked += B_Clicked;
                g3.Tapped += ChangeRecordingName;
                sl.Children.Add(b);

                f.Content = sl;
                RecordingPane.Children.Add(sl);
            }
        }

        private void CreateGroupRecordingsStack()
        {
            RecordingPane.Children.Add(new Label {Text = "Recordings Shared With You" });
            List<Group> groups = new List<Group>();
            using (var db = new Persistence.SQLiteDb())
            {
                groups = db.GetGroupbyUserID(user.ID);
            }
            foreach (Group group in groups)
            {
                List<GroupAudioRecord> gar;
                using (var db = new Persistence.SQLiteDb())
                {
                    gar = db.GetGroupAudioRecordByGroup(group.ID);
                }
                foreach (GroupAudioRecord groupAudio in gar)
                {
                    //string[] splitPath = recording.Split('/');
                    //string path = splitPath[4].Substring(0, splitPath[4].Length - 4);
                    Models.AudioRecord recording;
                    using (var db = new Persistence.SQLiteDb())
                    {
                        recording = db.GetAudioByID(groupAudio.AudioRecordId);
                    }
                    if (recording.CreatorId != user.ID)
                    {
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
                        sl.Children.Add(l);
                        f.Content = sl;
                        RecordingPane.Children.Add(sl);
                    }
                }
            }
        }
        private void B_Clicked(object sender, EventArgs e)
        {
            //get recordID, groupID and create list on 
            int index = RecordingPane.Children.IndexOf((StackLayout)((Button)sender).Parent);
            Models.AudioRecord record = user.Recordings[index];
            string groupName = ((Picker)RecordingsStackLayout.Children[2]).SelectedItem.ToString();
            if (user.Groups.Where(x => x.GroupName == groupName).ToList().Count() > 0)
            {

                Group g;
                int count = 0;
                using (var db = new Persistence.SQLiteDb())
                {
                    g = db.GetGroupByName(groupName);
                    count = db.GetGroupAudioRecords().Count();
                }
                GroupAudioRecord gar = new GroupAudioRecord
                {
                    AudioRecordId = record.ID,
                    GroupId = g.ID,
                    IsGroupAudioCreator = true
                };
                bool hasShared = false;
                using (var db = new Persistence.SQLiteDb())
                {
                    hasShared = db.CreateGroupAudioRecord(gar);
                }
                string retVal = hasShared ? "Audio shared" : "Something went wrong";
                DisplayAlert("", retVal, "Okay");
            }
            else
            {
                DisplayAlert("","That group doesn't exist", "Okay");
            }
            
        }

        private void ChangeRecordingName(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(EditEntry.Text.Trim()))
            {
                DisplayAlert("Error", "Invalid Name", "Ok");
            }
            else
            {
                StackLayout parent = (StackLayout)((Label)sender).Parent;
                int index = RecordingPane.Children.IndexOf(parent);
                string newName = EditEntry.Text;
                user.Recordings[index].Title = newName;
                using (var db = new Persistence.SQLiteDb())
                {
                    db.UpdateRecording(user.Recordings[index]);
                }
                DisplayRecordings();
            }
        }

        private void DeleteRecording(object sender, EventArgs e)
        {
            StackLayout parent = (StackLayout)((Xamarin.Forms.Image)sender).Parent;
            int index = RecordingPane.Children.IndexOf(parent);
            string newName = EditEntry.Text;
            Models.AudioRecord ar = user.Recordings[index];
            user.Recordings.RemoveAt(index);
            bool hasDeleted = false;
            using (var db = new Persistence.SQLiteDb())
            {
                db.DeleteGroupAudioRecordByAudioID(ar.ID);
                hasDeleted = db.DeleteRecording(ar);
            }
            string retVal = hasDeleted ? "Audio Deleted" : "Something went wrong";
            DisplayAlert("", retVal, "Okay");
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