using System.Windows.Forms;
using CefSharp.WinForms;

namespace RwdApp
{
	public partial class MainForm : Form
	{
		private readonly ChromiumWebBrowser _browser;

		public MainForm()
		{
			InitializeComponent();

			Text = "React Windows App";

			// Enabling this during devlopment can be a nice thing: this.TopMost = true;

			var availableBridges = new Bridges();

			string url = "file:///index.html";
			bool showDeveloperConsole = false;
#if DEBUG
			url = "http://localhost:3000";

			if (MessageBox.Show("You are currently in debug mode.\n\nMake sure to start your UI at localhost:3000 by running the command \"npm start\" in the RwdReactUI folder.\n\nDo you also want to open the developer console?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
			{
				showDeveloperConsole = true;
			};
#endif

			this.Controls.Add(new ReactHost(url, showDeveloperConsole, availableBridges.GetAvailableBridges())
			{
				Dock = DockStyle.Fill
			});
		}
	}
}