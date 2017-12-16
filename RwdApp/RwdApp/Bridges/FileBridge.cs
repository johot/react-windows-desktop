using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Dynamic;

namespace RwdApp
{
	public class FileBridge
	{
		public string[] GetDesktopFiles()
		{
			string path = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

			var files = Directory.GetFiles(path);

			return files;
		}
	}
}
