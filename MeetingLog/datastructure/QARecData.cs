using System;


namespace MeetingLog
{
	public class QARecData : IContent
	{

		public int Order
		{
			get;
			set;
		}

		public string Question
		{
			get;
			set;
		}

		public string Answer
		{
			get;
			set;
		}

		public string FileName
		{
			get;
			set;
		}

		public string ToJSON()
		{
			return null;
		}
	}
}
