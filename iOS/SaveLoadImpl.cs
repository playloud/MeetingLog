using System;
using Xamarin.Forms;
using MeetingLog;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

[assembly: Dependency(typeof(SaveLoadImpl))]
namespace MeetingLog
{
	public class SaveLoadImpl : ISaveLoad
	{

		public readonly string dbFolder = @"DB/";

		public string LoadText(string filename)
		{
			var documentsPath = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			var filePath = Path.Combine(documentsPath, filename);
			return System.IO.File.ReadAllText(filePath);
		}

		public void SaveText(string filename, string text)
		{
			var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			var filePath = Path.Combine(documentsPath, filename);
			System.IO.File.WriteAllText(filePath, text);
		}

		public string[] GetAllFileName()
		{
			List<string> fileNames = new List<string>();

			var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			var fileDirectory = Path.Combine(documentsPath, this.dbFolder);

			string[] fullPathes = Directory.GetFiles(fileDirectory);

			fullPathes.ToList().ForEach(a => {
				FileInfo fi = new FileInfo(a);
				fileNames.Add(fi.Name);
			});

			return fileNames.ToArray();
		}

		public void SaveData(string data)
		{
			string fileName = string.Format("meetingLog_{0}", DateTime.Now.ToString("hhmmss"));
			var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

			// if directory not found, create it
			var tempPath = Path.Combine(documentsPath, dbFolder);
			if (!Directory.Exists(tempPath))
				Directory.CreateDirectory(tempPath);

			var filePath = Path.Combine(documentsPath, dbFolder, fileName);
			System.IO.File.WriteAllText(filePath, data);
		}

        public FileNameContent[] GetAllFileContents()
		{
			var documentsPath = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			var filePath = Path.Combine(documentsPath, this.dbFolder);

			if (!   Directory.Exists(filePath))
				Directory.CreateDirectory(filePath);
			         
			string[] allPath = Directory.GetFiles(filePath);
			List<FileNameContent> contents = new List<FileNameContent>();
			allPath.ToList().ForEach(a => contents.Add(new FileNameContent() { FilePath = a, FileContent=File.ReadAllText(a)}));
			return contents.ToArray();
		}

		public List<Meeting> GetAllMeetingObject()
		{
			List<Meeting> meetings = new List<Meeting>();

			FileNameContent[] allContents = GetAllFileContents();

			allContents.ToList().ForEach(a =>
			{
				try
				{
                    Meeting meeting = JsonConvert.DeserializeObject<Meeting>(a.FileContent);
                    meeting.filePathSaved = a.FilePath;
					meetings.Add(meeting);
				}
				catch
				{

				}

			});
			return meetings;
		}

        public void RemoveFile(string path)
        {
            File.Delete(path);
        }

		public string[] GetAllFileNames()
		{
			return GetAllFileName();
        }

        // helper class for loading Meeting object
        public class FileNameContent
        {
            public string FilePath
            {
                get;
                set;
            }

            public string FileContent
            {
                get;
                set;
            }

        }
	}
}
