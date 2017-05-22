using System;
using Foundation;
using System.IO;
using AVFoundation;
using Speech;
using Xamarin.Forms;
using MeetingLog.iOS;
using MonoTouch;

[assembly: Dependency(typeof(VoiceRecognition))]
namespace MeetingLog.iOS
{
	public class VoiceRecognition : IVoiceRecognition
	{
		public VoiceRecognition()
		{
		}

		bool isTranslatingDone = false;
		string resultString = null;

        public string RecognizeVoice(string path, Editor editor)
		{

			SFSpeechRecognizer recognizer = new SFSpeechRecognizer(NSLocale.CurrentLocale);

			 // Is the default language supported?
		    if (recognizer == null) {
		        // No, return to caller
		        return "recognizer is null";
		    }

		    // Is recognition available?
		    if (!recognizer.Available) {
		        // No, return to caller
		        return "recognizer is not available";
		    }
            isTranslatingDone = false;
			NSUrl url = new NSUrl(path, false);
			SFSpeechUrlRecognitionRequest request = new SFSpeechUrlRecognitionRequest(url);
            SFSpeechRecognitionTask sptask = recognizer.GetRecognitionTask(request, (SFSpeechRecognitionResult result, NSError err) => 
			{

				if (err != null)
				{
					resultString = err.Description;
                    isTranslatingDone = true;
                    editor.Text = resultString;
				}
				else
				{
					if (result != null)
					{
						if (result.Final)
						{
							resultString = result.BestTranscription.FormattedString;
							isTranslatingDone = true;
                            // call back??
                            editor.Text = resultString;
						}
					}
				}
			});
            return "cannot recognize";
		}

		public string GetResult()
		{
			return resultString;
		}


	}
}
