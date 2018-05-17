using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace innaMinute.Pages
{
				[XamlCompilation(XamlCompilationOptions.Compile)]
				public partial class CreateAccountPage : ContentPage
				{
								public CreateAccountPage()
								{
												InitializeComponent();
								}

								private void CreateAccountBtn_Clicked(object sender, EventArgs e)
								{

								}

								private void PasswordEntry_TextChanged(object sender, TextChangedEventArgs e)
								{

								}
				}
}