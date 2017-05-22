using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using Xamarin.Forms;

namespace MeetingLog
{
	public class MeetingDepot
	{
		private static MeetingDepot instance = null;
		private List<Meeting> meetings = null;

		private MeetingDepot()
		{
			if (meetings == null)
				meetings = new List<Meeting>();
		}


		public static MeetingDepot GetInstance()
		{
			if (instance == null)
			{
				instance = new MeetingDepot();
			}

			return instance;
		}

		public void AddMeetingData(Meeting data)
		{
			this.meetings.Add(data);
		}

		public List<Meeting> GetMeetings()
		{
			return meetings;
		}

		// for matching list view
		public ObservableCollection<Meeting> GetDataAsObseravable()
		{
			var toReturn = new ObservableCollection<Meeting>();
			foreach (var meeting in this.meetings)
				toReturn.Add(meeting);
			return toReturn;
		}

		public void LoadFromSave()
		{
			// load all files



		}

		public void SaveAll()
		{
			
		}

		public void Save(string fileName,Meeting data)
		{
			DependencyService.Get<ISaveLoad>().SaveText(fileName, data.ToJSON());

		}

		internal void UpdateMeetingData(Meeting meeting)
		{
			var query = meetings.Where(a => a.UID == meeting.UID);
			if (query.Any())
			{
				Meeting oldObject = query.FirstOrDefault();
				oldObject = meeting;
			}
		}

		internal Meeting FindDataByTitle(string selectedTitle)
		{

			var query = meetings.Where(a => a.Title == selectedTitle);
			if (query.Any())
				return query.FirstOrDefault();

			return null;
		}

}




}
