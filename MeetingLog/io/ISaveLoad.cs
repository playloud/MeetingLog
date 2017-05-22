using System;
using System.Linq;
using MeetingLog;
using System.Collections.Generic;

namespace MeetingLog
{
	public interface ISaveLoad
	{
		void SaveText(string filename, string text);

		void SaveData(string data);

		string LoadText(string filename);

		string[] GetAllFileContents();

		string[] GetAllFileNames();

		List<Meeting> GetAllMeetingObject();


	}
}
