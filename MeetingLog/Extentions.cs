using System;
namespace MeetingLog
{

	public static class Extentions
	{
		public static string GetUID()
		{
			string uid = string.Empty;

			uid = DateTime.Now.ToString("u");

			return uid;
		}

		public static string ToMinSecondString(this int i)
		{

			int seconds = i % 60;
			int minutes = i / 60;

			return string.Format("{0}:{1}", minutes, seconds);
		}

        public static string ToSomething(this int t)
        {
            return "test";
        }
	}


}
