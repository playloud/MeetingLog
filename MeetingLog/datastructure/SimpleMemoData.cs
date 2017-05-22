using System;
using Newtonsoft.Json;

namespace MeetingLog
{
	public class SimpleMemoData : IContent
	{

		public int Order
		{
			get;
			set;
		}

		public string Memo
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
