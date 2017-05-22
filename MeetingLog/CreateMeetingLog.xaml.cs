using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace MeetingLog
{
	public partial class CreateMeetingLog : ContentPage
	{

		string[] popupMenues = { "Question-Answer", "Simple Memo", "Feeling" };

		int order = 0;
		List<Object> compoments = null;

		public MeetingLogPage parentHandle = null;
		Meeting data;
		bool IsNew = false;

		public CreateMeetingLog()
		{
			InitializeComponent();
			SetupEvents();
			IsNew = true;
		}

		public CreateMeetingLog(Meeting data)
		{
			InitializeComponent();
			this.data = data;
			LoadWithMeeting(data);

			SetupEvents();
			IsNew = false;
		}

		public void SetupEvents()
		{
			compoments = new List<object>();

			var buttonPop = this.FindByName<Button>("buttonAddNewElement");
			buttonPop.Clicked += (sender, e) =>
			{
				this.PopupAddTemplate(sender, e);
			};

			buttonCompleteMeeting.Clicked += (sender, e) =>
			{
				Meeting meeting = null;

				if (IsNew)
				{
					meeting = new Meeting();
					meeting.UID = Extentions.GetUID();
				}
				else
				{
					meeting = this.data;
					meeting.Clear();
				}


				foreach (var comps in compoments)
				{
					if (comps is Feeling)
						meeting.AddFeeling(((Feeling)comps).GetData());
					else if (comps is QARec)
						meeting.AddQARec(((QARec)comps).GetData());
					else if (comps is SimpleMemo)
						meeting.AddSimpleMemo(((SimpleMemo)comps).GetData());
				}
				meeting.Title = this.title.Text;

				if (IsNew)
				{
					MeetingDepot.GetInstance().AddMeetingData(meeting);
					// now save the data
					DependencyService.Get<ISaveLoad>().SaveData(meeting.ToJSON());
					parentHandle.LoadMeetingLogs();
					//parentHandle.AddSomething();

				}
				else
				{
					//MeetingDepot.GetInstance().AddMeetingData(meeting);
					MeetingDepot.GetInstance().UpdateMeetingData(meeting);
					// now save the data
					DependencyService.Get<ISaveLoad>().SaveData(meeting.ToJSON());
					parentHandle.LoadMeetingLogs();
					//parentHandle.AddSomething();
				}


				Navigation.PopAsync();

			};
		}


		//get over with reading object
		public void LoadWithMeeting(Meeting data)
		{

			this.title.Text = data.Title;

			foreach (FeelingData feelingData in data.feelings)
			{
				Feeling feel = new Feeling(feelingData);
				this.FindByName<StackLayout>("templateHolder").Children.Add(feel);
			}

			foreach (QARecData qarecData in data.qarecs)
			{
				QARec qarec = new QARec(qarecData);
				this.FindByName<StackLayout>("templateHolder").Children.Add(qarec);
			}

			foreach (SimpleMemoData sdata in data.simpleMemos)
			{
				SimpleMemo sm = new SimpleMemo(sdata);
				this.FindByName<StackLayout>("templateHolder").Children.Add(sm);
			}
		}

		async void PopupAddTemplate(object sender, EventArgs e)
		{
			var action = await DisplayActionSheet("Add New template", "Cancel", null, popupMenues);
			Debug.WriteLine("Action: " + action);

			if (action == popupMenues[0])
			{
				// question - answer
				QARec qar = new QARec();
				qar.Order = order++;
				compoments.Add(qar);
				this.FindByName<StackLayout>("templateHolder").Children.Add(qar);
			}
			else if (action == popupMenues[1])
			{
				// simple memo
				SimpleMemo sm = new SimpleMemo();
				sm.Order = order++;
				compoments.Add(sm);
				var stack = this.FindByName<StackLayout>("templateHolder");
				stack.Children.Add(sm);
			}
			else if (action == popupMenues[2])
			{
				// feeling
				Feeling fe = new Feeling();
				fe.Order = order++;
				compoments.Add(fe);
				this.FindByName<StackLayout>("templateHolder").Children.Add(fe);
			}
		}

		public void AddQARec()
		{

		}

		public void AddSimpleMemo()
		{

		}

		public void AddFeeling()
		{

		}
	}
}
