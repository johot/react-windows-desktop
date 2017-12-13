using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;

namespace RwdApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var settings = new CefSettings() { CachePath = "Cache" };
			
	        //settings.CefCommandLineArgs.Add("enable-npapi", "1"); //Enable NPAPI plugs which were disabled by default in Chromium 43 (NPAPI will be removed completely in Chromium 45)

			//settings.UserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 9_1 like Mac OS X) AppleWebKit/601.1.46 (KHTML, like Gecko) Version/9.0 Mobile/13B143 Safari/601.1";

			// settings.CefCommandLineArgs.Add("disable-gpu", "1");

			//Cef.EnableHighDPISupport();

			//var settings = new CefSettings()
			//{
			//    //By default CefSharp will use an in-memory cache, you need to specify a Cache Folder to persist data
			//    CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\Cache")
			//};

			////Perform dependency check to make sure all relevant resources are in our output directory.
			//Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);

			Cef.EnableHighDPISupport();
            Cef.Initialize(settings);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());

			// Cleanup
			Cef.Shutdown();
        }
    }
}
