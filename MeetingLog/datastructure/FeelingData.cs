using System;
using Newtonsoft.Json;

namespace MeetingLog
{
	public class FeelingData : IContent
	{
		public int Order
		{
			get;
			set;
		}

		public string Feeling
		{
			get;
			set;
		}

		public string ToJSON()
		{
			var json = JsonConvert.SerializeObject(this);
			return json;
		}
	}
}
