using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MeetingLog
{
	public partial class RecordedView : ContentView
	{

		IPlayer player = null;
		IVoiceRecognition voiceRecog = null;
		string recordFilePath = null;

		public RecordedView()
		{
			InitializeComponent();

		}

		public RecordedView(string rFilePath, int time)
		{
			recordFilePath = rFilePath;

			InitializeComponent();

            this.buttonPlay.Clicked += PlayRecorded;
            this.buttonStop.Clicked += StopRecorded;
            this.buttonPause.Clicked += PauseRecorded;

            //this.buttonGetVoiceText.Clicked += getVoiceText;
            this.buttonGetVoiceText.Clicked += async (sender, e) =>
            {
                entryRecognizedText.Text = "loading the result";
                //Task<string> tresult = GetVoiceText_async(rFilePath);
                voiceRecog = DependencyService.Get<IVoiceRecognition>();
                voiceRecog.RecognizeVoice(rFilePath, entryRecognizedText);
                //string recogText = await tresult;
                //entryRecognizedText.Text = recogText;
            };

			//this.labelFileName.Text = rFilePath;
            this.labelRecordedTime.Text = time.ToMinSecondString();
            player = DependencyService.Get<IPlayer>();
		}

        // not used
        //void getVoiceText(object sender, EventArgs e)
        //{
        //	if (this.recordFilePath != null)
        //	{

        //		if (voiceRecog == null)
        //		{
        //			voiceRecog = DependencyService.Get<IVoiceRecognition>();
        //			string result = voiceRecog.RecognizeVoice(recordFilePath);
        //			entryRecognizedText.Text = result;

        //		}
        //		else
        //		{
        //			entryRecognizedText.Text = voiceRecog.GetResult();
        //		}
        //	}
        //}

  //      public async Task<string> GetVoiceText_async(string recordedFilePath)
  //      {
  //          voiceRecog = DependencyService.Get<IVoiceRecognition>();
		//	return voiceRecog.RecognizeVoice(recordFilePath);
		//}

		void PlayRecorded(object sender, EventArgs e)
		{
			player.Init(recordFilePath);
			player.Play();
		}

		void StopRecorded(object sender, EventArgs e)
		{
			player.Stop();
		}

		void PauseRecorded(object sender, EventArgs e)
		{

		}
	}
}
