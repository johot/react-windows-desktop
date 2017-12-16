using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp.WinForms;
using CefSharp;
using Newtonsoft.Json;

namespace RwdApp
{
	public partial class ReactHost : UserControl
	{
		private ChromiumWebBrowser _browser;
		private bool _showDeveloperConsole;

		public ReactHost(string url, bool showDeveloperConsole, IEnumerable<object> bridges)
		{
			InitializeComponent();

			_showDeveloperConsole = showDeveloperConsole;

			_browser = new ChromiumWebBrowser("")
			{
				Dock = DockStyle.Fill
			};

			_browser.IsBrowserInitializedChanged += _browser_IsBrowserInitializedChanged;

			//_browser.LoadingStateChanged += Browser_LoadingStateChanged;
			_browser.ContextMenu = null;

			_browser.RegisterAsyncJsObject("bridgeMediator", new BridgeMediator(_browser, bridges));

			//foreach (var bridge in bridges)
			//{
			//	_browser.RegisterAsyncJsObject(bridge.GetType().Name, bridge);
			//}

			//_browser.AddressChanged += _browser_AddressChanged;

			_browser.LifeSpanHandler = new LifeSpanHandler();

			this.Load += ReactHost_Load;
			this.Controls.Add(_browser);

			_browser.Load(url);
		}

		private void _browser_IsBrowserInitializedChanged(object sender, IsBrowserInitializedChangedEventArgs e)
		{
			if (_showDeveloperConsole)
			{
				_browser.ShowDevTools();
			}
		}

		private void ReactHost_Load(object sender, EventArgs e)
		{

		}
	}

	
}
