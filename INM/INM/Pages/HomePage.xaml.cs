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
	public partial class HomePage : ContentPage
	{
		User user;
		public HomePage (User user)
		{
			InitializeComponent ();
			this.user = user;
		}

		private void RecordButton_Tapped(object sender, EventArgs e)
		{
			
		}

		private void ContactsToolbarItem_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new ContactsPage(user));
		}
	}
}