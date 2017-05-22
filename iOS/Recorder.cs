using System;
using Foundation;
using System.IO;
using AVFoundation;
using AudioToolbox;
using Xamarin.Forms;
using MeetingLog.iOS;

[assembly: Dependency(typeof(Recorder))]
namespace MeetingLog.iOS
{
	public class Recorder: IRecorder
	{
		public Recorder()
		{
		}

		NSUrl url = null;
		AVAudioRecorder recorder = null;
		NSError error = null;

		public bool Init(ref string errMsg)
		{

			var audioSession = AVAudioSession.SharedInstance();

			audioSession.RequestRecordPermission(delegate(bool granted) {
                Console.WriteLine("Audio Permission:"+granted);
            });


			var err = audioSession.SetCategory(AVAudioSessionCategory.PlayAndRecord); 

			if (err != null) 
			{ 
				Console.WriteLine ("audioSession: {0}", err);
				errMsg = string.Format("audio session  {0}", err);
				return false;
			} 
			err = audioSession.SetActive (true); 

			if (err != null )
			{ 
				Console.WriteLine ("audioSession: {0}", err); 
				errMsg = string.Format("audio session  {0}", err);
				return false;
				//return;
			}
			return true;
		}

		public void Pause()
		{
			if (recorder != null)
				recorder.Pause();
			
		}

		public bool StartRecord(ref string errMsg, ref string audioFilePath)
		{

			if (!Init(ref errMsg))
				return false;

			//Declare string for application temp path and tack on the file extension 
			string fileName = string.Format("Myfile{0}.wav", DateTime.Now.ToString("yyyyMMddHHmmss"));
			audioFilePath = Path.Combine(Path.GetTempPath(), fileName);

			Console.WriteLine("Audio File Path: " + audioFilePath);

			url = NSUrl.FromFilename(audioFilePath);

			//set up the NSObject Array of values that will be combined with the keys to make the NSDictionary 
			NSObject[] values = new NSObject[] {

				//Sample Rate 
				NSNumber.FromFloat (44100.0f), 

				//AVFormat 
				NSNumber.FromInt32 ((int)AudioToolbox.AudioFormatType.LinearPCM), 

				//Channels 
				NSNumber.FromInt32 (2), 

				//PCMBitDepth 
				NSNumber.FromInt32 (16), 

				//IsBigEndianKey 
				NSNumber.FromBoolean (false), 

				//IsFloatKey 
				NSNumber.FromBoolean (false) 
			};


			//Set up the NSObject Array of keys that will be combined with the values to make the NSDictionary 
			NSObject[] keys = new NSObject[] {
				AVAudioSettings.AVSampleRateKey,
				AVAudioSettings.AVFormatIDKey,
				AVAudioSettings.AVNumberOfChannelsKey,
				AVAudioSettings.AVLinearPCMBitDepthKey,
				AVAudioSettings.AVLinearPCMIsBigEndianKey,
				AVAudioSettings.AVLinearPCMIsFloatKey
			};

			//Set Settings with the Values and Keys to create the 
			NSDictionary settings = NSDictionary.FromObjectsAndKeys(values, keys);


			if (IsDevice())
			{
				//Set recorder parameters 
				recorder = AVAudioRecorder.Create(url, GetSettings_device_02(), out error);
			}
			else
			{
				//Set recorder parameters 
	 			recorder = AVAudioRecorder.Create(url, new AudioSettings(settings), out error);
			}

			if(error != null)
				Console.WriteLine("M Love: "+error.Description);

			bool isPrepared = recorder.PrepareToRecord();

			if (isPrepared)
			{
				recorder.Record();
				return true;
			}

			return false;
		}



		public void Stop()
		{
			if (recorder != null)
				recorder.Stop();
		}

		public double GetCurrenttime()
		{

			if (recorder != null)
			{
				return recorder.currentTime;
			}

			return 0;
		}


		public AudioSettings GetAudioSettings_simul()
		{
			AudioSettings asettings = new AudioSettings()
			{
				SampleRate = 44100.0f,
				Format = AudioToolbox.AudioFormatType.LinearPCM,
				NumberChannels = 2,
				LinearPcmBitDepth = 16,
				LinearPcmBigEndian = false,
				LinearPcmFloat = false
			};
			return asettings;
		}

		public AudioSettings GetAudioSettings_device()
		{
			AudioSettings asettings = new AudioSettings()
			{
				SampleRate = 44100.0f,
				Format = AudioToolbox.AudioFormatType.LinearPCM,
				NumberChannels = 2,
				LinearPcmBitDepth = 16,
				LinearPcmBigEndian = false,
				LinearPcmFloat = false
			};
			return asettings;
		}


		public AudioSettings GetSettings_device_02()
		{
			AudioSettings settings = new AudioSettings()
			{
				SampleRate = 16000.0f,
				Format = AudioFormatType.LinearPCM,
				NumberChannels = 1,
				AudioQuality = AVAudioQuality.Medium
			};

			return settings;
		}

		public bool IsSimulator()
		{
			
			return (ObjCRuntime.Runtime.Arch == ObjCRuntime.Arch.SIMULATOR);
			//return UIDevice.CurrentDevice.Model.Contains("Simulator");
		}

		public bool IsDevice()
		{
			return (ObjCRuntime.Runtime.Arch == ObjCRuntime.Arch.DEVICE);
		}



	}
}
