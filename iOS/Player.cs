using System;
using AVFoundation;
using AudioToolbox;
using Foundation;
using UIKit;
using Xamarin.Forms;
using MeetingLog.iOS;

[assembly: Dependency(typeof(Player))]
namespace MeetingLog.iOS
{
	public class Player : IPlayer
	{
		public Player()
		{
		}

		string filePath = null;
		private AVAudioPlayer backgroundMusic;
		private AVAudioPlayer soundEffect;

		public float MusicVolume { get; set; } = 0.5f;

		public void Init(string filePath)
		{
			this.filePath = filePath;
		}

		public void Play()
		{
			NSUrl songURL;


            // Any existing background music?
            if (backgroundMusic!=null) {
                //Stop and dispose of any background music
                backgroundMusic.Stop();
                backgroundMusic.Dispose();
            }

            // Initialize background music
			songURL = new NSUrl(filePath);
			NSError err;
			backgroundMusic = new AVAudioPlayer(songURL, "wav", out err);
			backgroundMusic.Volume = MusicVolume;
            backgroundMusic.FinishedPlaying += delegate { 
                // backgroundMusic.Dispose(); 
                backgroundMusic = null;
            };
            backgroundMusic.NumberOfLoops=-1;
            backgroundMusic.Play();
            
		}

		public void Stop()
		{
			if (backgroundMusic!=null) {
                backgroundMusic.Stop();
                backgroundMusic.Dispose();
            }
		}
	}
}
