using System;
namespace MeetingLog
{
	public interface IPlayer
	{

		void Init(string filePath);

		void Play();

		void Stop();

	}
}
