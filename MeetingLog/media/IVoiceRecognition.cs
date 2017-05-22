using System;
using Xamarin.Forms;
namespace MeetingLog
{
	public interface IVoiceRecognition
	{
		string RecognizeVoice(string path, Editor edit);
		string GetResult();
	}
}
