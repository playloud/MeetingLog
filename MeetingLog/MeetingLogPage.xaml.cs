using Xamarin.Forms;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace MeetingLog
{
	public partial class MeetingLogPage : ContentPage
	{
		public MeetingLogPage()
		{
			InitializeComponent();

			// list view cell template
			meetingList.ItemTemplate = new DataTemplate(typeof(MLCell));


			// add tool bar items
			ToolbarItem tbi = new ToolbarItem();
			tbi.Text = "New";

			this.ToolbarItems.Add(tbi);

			tbi.Clicked += (sender, e) =>
			{
				CreateMeetingLog creator = new CreateMeetingLog();
				creator.parentHandle = this;
				Navigation.PushAsync(creator, true);
			};

			buttonAddToList.Clicked += (sender, e) =>
			{
				AddSomething();
				//LoadMeetingLogs();
			};

			buttonSaveTextTest.Clicked += (sender, e) =>
			{
				DependencyService.Get<ISaveLoad>().SaveText("tempFile.txt", "this is test contents");
			};

			// when the meeting list clicked
			this.meetingList.ItemTapped += (sender, e) =>
			{
				Meeting data = (Meeting)meetingList.SelectedItem;
				CreateMeetingLog creator = new CreateMeetingLog(data);
				creator.parentHandle = this;
				Navigation.PushAsync(creator, true);
			};

			LoadMeetingLogs();
		}

		protected override void OnAppearing()
		{
			//LoadMeetingLogs();
		}


		public void LoadMeetingLogs()
		{
			// refresh items
			this.meetingList.ItemsSource = null;
			this.meetingList.ItemsSource = MeetingDepot.GetInstance().GetDataAsObseravable();
		}

		public void AddSomething()
		{
			List<Meeting> meetings = DependencyService.Get<ISaveLoad>().GetAllMeetingObject();
			int count = meetings.Count;
		}


	}

	public class MLCell : ViewCell
	{
		public MLCell()
		{
			//instantiate each of our views
			//var image = new Image();
			var LabelName = new Label();
			var LabelUID = new Label();
			var verticaLayout = new StackLayout();
			//var horizontalLayout = new StackLayout() { BackgroundColor = Color.Navy };
			var horizontalLayout = new StackLayout();

			//set bindings
			LabelName.SetBinding(Label.TextProperty, new Binding("Title"));
			LabelUID.SetBinding(Label.TextProperty, new Binding("UID"));
			//image.SetBinding(Image.SourceProperty, new Binding("Image"));

			//Set properties for desired design
			horizontalLayout.Orientation = StackOrientation.Horizontal;
			horizontalLayout.HorizontalOptions = LayoutOptions.Fill;
			//image.HorizontalOptions = LayoutOptions.End;
			LabelName.FontSize = 20;
			LabelUID.FontSize = 10;

			//add views to the view hierarchy
			verticaLayout.Children.Add(LabelName);
			verticaLayout.Children.Add(LabelUID);
			horizontalLayout.Children.Add(verticaLayout);
			//horizontalLayout.Children.Add(image);

			// add to parent view
			View = horizontalLayout;


			/// CONTEXT ACTIONS..

			var moreAction = new MenuItem { Text = "More" };
			moreAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
			moreAction.Clicked += (sender, e) =>
			{
				var mi = ((MenuItem)sender);
				//Debug.WriteLine("More Context Action clicked: " + mi.CommandParameter);
			};

			var deleteAction = new MenuItem { Text = "Delete", IsDestructive = true }; // red background
			deleteAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
			deleteAction.Clicked += (sender, e) =>
			 {
				 var mi = ((MenuItem)sender);
				 Debug.WriteLine("Delete Context Action clicked: " + mi.CommandParameter);
			 };

			ContextActions.Add(moreAction);
			ContextActions.Add(deleteAction);
		}
	}
}
