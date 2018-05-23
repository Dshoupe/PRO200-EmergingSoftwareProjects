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
	public partial class ProfilePage : ContentPage
	{
		User user;
		public ProfilePage (User user)
		{
			InitializeComponent ();
			this.user = user;
			ProfilePageGrid.BindingContext = user;
		}
	}
}