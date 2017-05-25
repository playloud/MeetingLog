using Xamarin.Forms;

namespace MeetingLog
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
            MeetingDepot.GetInstance().LoadFromSave();
			NavigationPage nav = new NavigationPage(new MeetingLogPage());
			MainPage = nav;
		}

		protected override void OnStart()
		{
            
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
