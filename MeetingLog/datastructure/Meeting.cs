using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MeetingLog
{
	public class Meeting : IContent
	{
		public string UID
		{
			get;
			set;
		}

		public string Title
		{
			get;
			set;
		}

		public DateTime LastModified
		{
			get;
			set;
		}

		public List<FeelingData> feelings = null;
		public List<QARecData> qarecs = null;
		public List<SimpleMemoData> simpleMemos = null;

		public Meeting()
		{
			LastModified = DateTime.Now;

			feelings = new List<FeelingData>();
			qarecs = new List<QARecData>();
			simpleMemos = new List<SimpleMemoData>();
		}

		public void AddFeeling(FeelingData data)
		{
			if (feelings == null)
				feelings = new List<FeelingData>();

			feelings.Add(data);
		}

		public void AddQARec(QARecData data)
		{
			if (qarecs == null)
				qarecs = new List<QARecData>();
			qarecs.Add(data);
		}

		public void AddSimpleMemo(SimpleMemoData data)
		{
			if (simpleMemos == null)
				simpleMemos = new List<SimpleMemoData>();

			simpleMemos.Add(data);
		}

		internal void Clear()
		{
			feelings.Clear();
			qarecs.Clear();
			simpleMemos.Clear();
		}

		public string ToJSON()
		{
			string result = JsonConvert.SerializeObject(this, Formatting.Indented);
			return result;	
		}

}

	
}
