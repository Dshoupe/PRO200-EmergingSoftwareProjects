using INM.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace INM
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
			
			MainPage = new NavigationPage(new MainPage())
			{
				BarBackgroundColor = Color.FromHex("#D7CABD"),
				BarTextColor = Color.Black,
				
				
			};
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}


		protected override void OnResume()
		{
			// Handle when your app resumes
		}

		//private void Create10RandomUsers()
		//{
		//	for(int i =0; i < 10; i++)
		//	{
		//		User user = new User()
		//		{
		//			FirstName = $"Tester{i + 1}",
		//			LastName = $"Last{i + 1}",
		//			Email = $"testEmail{i}@test.com",
		//			Password = $"test1234{i}",
		//			PhoneNumber = $"{i}233232345",
		//			Username = $"Tester{i}"
		//		};
		//		using (var db = new Persistence.SQLiteDb())
		//		{
		//			db.CreateUser(user)
		//		}
		//	}
		//}
	}
}
