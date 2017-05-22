using System;
namespace MeetingLog
{
	public interface IRecorder
	{
		bool StartRecord(ref string errMsg, ref string fileName);

		void Pause();

		void Stop();

		double GetCurrenttime();

	}
}
