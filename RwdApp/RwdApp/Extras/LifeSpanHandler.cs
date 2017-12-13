using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RwdApp
{
    public class LifeSpanHandler : ILifeSpanHandler
    {
        public bool DoClose(IWebBrowser browserControl, IBrowser browser)
        {
            return true;
        }

        public void OnAfterCreated(IWebBrowser browserControl, IBrowser browser)
        {
        }

        public void OnBeforeClose(IWebBrowser browserControl, IBrowser browser)
        {
        }

        public bool OnBeforePopup(IWebBrowser browserControl, IBrowser browser, IFrame frame, string targetUrl, string targetFrameName, WindowOpenDisposition targetDisposition, bool userGesture, IPopupFeatures popupFeatures, IWindowInfo windowInfo, IBrowserSettings browserSettings, ref bool noJavascriptAccess, out IWebBrowser newBrowser)
        {
            newBrowser = null;

            browserControl.Load(targetUrl);
            
            return true;
        }
    }

	//public class MyBrowser : ChromiumWebBrowser
	//{
	//	public MyBrowser(string address) : base(address)
	//	{
	//	}
	//}

	//public class CallbackObjectForJs
	//{
	//	public void showMessage(string msg)
	//	{
	//		//Read Note
	//		MessageBox.Show(msg);
	//	}
	//}

	//public class CustomMenuHandler : IContextMenuHandler
	//{
	//	public void OnBeforeContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame,
	//		IContextMenuParams parameters, IMenuModel model)
	//	{
	//		model.Clear();
	//	}

	//	public bool OnContextMenuCommand(IWebBrowser browserControl, IBrowser browser, IFrame frame,
	//		IContextMenuParams parameters, CefMenuCommand commandId, CefEventFlags eventFlags)
	//	{
	//		return false;
	//	}

	//	public void OnContextMenuDismissed(IWebBrowser browserControl, IBrowser browser, IFrame frame)
	//	{
	//	}

	//	public bool RunContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame,
	//		IContextMenuParams parameters, IMenuModel model, IRunContextMenuCallback callback)
	//	{
	//		return false;
	//	}
	//}
}
