using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MeetingLog
{
	public partial class QARec : ContentView
	{
		public int Order
		{
			get;
			set;
		}

		IRecorder rec = null;

		List<RecordedView> records = null;

		string currentRecordedFile = null;

		int currentRecordSeconds = 0;
		bool isTimerOn = false;

		public QARec()
		{
			InitializeComponent();

			rec = DependencyService.Get<IRecorder>();

			buttonRec.Clicked += StartRecordClicked;
			buttonStop.Clicked += StopClicked;
			buttonPause.Clicked += ButtonPause_Clicked;

			records = new List<RecordedView>();
		}

		public QARec(QARecData data)
		{
			InitializeComponent();

			this.Order = data.Order;
			this.editorAnswer.Text = data.Answer;
			this.editorQuestion.Text = data.Question;

			buttonRec.Clicked += StartRecordClicked;
			buttonStop.Clicked += StopClicked;
			buttonPause.Clicked += ButtonPause_Clicked;

			records = new List<RecordedView>();
		}

		public QARecData GetData()
		{
			QARecData data = new QARecData();

			data.Order = Order;
			data.Answer = this.editorAnswer.Text;
			data.Question = this.editorQuestion.Text;

			return data;
		}

		public void StartRecordClicked(object sender, EventArgs e)
		{
			string errMsg = null;
			string filename = null;

			if (rec.StartRecord(ref errMsg, ref filename))
			{
				// label updates, 
				labelRecStatus.Text = "Recording..";
				currentRecordedFile = filename;
				currentRecordSeconds = 0;
				isTimerOn = true;
				Xamarin.Forms.Device.StartTimer(new TimeSpan(0, 0, 1), UpdateRecordStatus);
                buttonRec.BackgroundColor = Color.Red;
			}
			else
			{
				// update status label
				labelRecStatus.Text = errMsg;


			}
		}

		bool UpdateRecordStatus()
		{
			++currentRecordSeconds;
			labelRecTime.Text = currentRecordSeconds.ToMinSecondString();
			if (isTimerOn)
				return true;
			return false;
		}

		void StopClicked(object sender, EventArgs e)
		{
			if (rec != null)
			{
				rec.Stop();
				labelRecStatus.Text = "stopped";
				isTimerOn = false;

				// Add New RecordedView
                RecordedView rview = new RecordedView(this.currentRecordedFile, this.currentRecordSeconds);
                slayout.Children.Add(rview);

                buttonRec.BackgroundColor = buttonStop.BackgroundColor;
			}
		}

		void ButtonPause_Clicked(object sender, EventArgs e)
		{
			if (rec != null)
			{
				rec.Pause();
				labelRecStatus.Text = "paused";
			}
		}
	}
}
