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
	public partial class AddContactPage : ContentPage
	{
		public User user;
		public AddContactPage (User user)
		{
			InitializeComponent ();
			this.user = user;
		}

		private void SearchButton_Clicked(object sender, EventArgs e)
		{

		}
	}
}