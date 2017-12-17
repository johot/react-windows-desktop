using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp;
using CefSharp.WinForms;
using Newtonsoft.Json.Serialization;
using System.Web;

namespace RwdApp
{
	public class BridgeMediator
	{
		private Dictionary<string, object> _bridges = new Dictionary<string, object>();
		private ChromiumWebBrowser _browser;

		public BridgeMediator(ChromiumWebBrowser browser, IEnumerable<object> bridges)
		{
			_browser = browser;

			foreach (var bridge in bridges)
			{
				string bridgeName = ConvertToCamelCase(bridge.GetType().Name);
				_bridges.Add(bridgeName, bridge);
				HookUpEventListeners(bridgeName, bridge);
			}
		}

		private void HookUpEventListeners(string bridgeName, object instance)
		{
			var eventInfos = instance.GetType().GetEvents();

			foreach (var eventInfo in eventInfos)
			{
				if (eventInfo.EventHandlerType == typeof(Action))
				{
					eventInfo.AddEventHandler(instance, new Action(() => HandleEvent(bridgeName, eventInfo.Name, null)));
				}
				else
				{
					eventInfo.AddEventHandler(instance, new Action<object>(arg => HandleEvent(bridgeName, eventInfo.Name, arg)));
				}
			}
		}

		public void HandleEvent(string bridgeName, string eventName, object args)
		{
			var argsJson = JsonConvert.SerializeObject(args, new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver()
			});

			argsJson = HttpUtility.JavaScriptStringEncode(argsJson);

			_browser.ExecuteScriptAsync("window.bridgeManager.getBridge('" + bridgeName + "').then(bridge => bridge.triggerEvent('" + ConvertToCamelCase(eventName) + "', JSON.parse('" + argsJson + "')))");
		}

		public string CallMethod(string bridgeName, string methodName, string arguments)
		{
			var realArguments = JsonConvert.DeserializeObject<object[]>(arguments);

			var bridgeObject = _bridges[bridgeName];
			var mi = bridgeObject.GetType().GetMethod(ConvertToTitleCase(methodName));
			object result = mi.Invoke(bridgeObject, realArguments);

			return JsonConvert.SerializeObject(result, new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver()
			});
		}

		public string GetBridgeMethods(string bridgeName)
		{
			var bridgeObject = _bridges[bridgeName];

			// Return method names
			var methods = bridgeObject.GetType().GetMethods().Select(mi => mi.Name).Select(methodName => ConvertToCamelCase(methodName)).ToList();
			
			return JsonConvert.SerializeObject(methods, new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver()
			});
		}

		private string ConvertToTitleCase(string name)
		{
			return Char.ToUpperInvariant(name[0]) + name.Substring(1);
		}

		private string ConvertToCamelCase(string name)
		{
			return Char.ToLowerInvariant(name[0]) + name.Substring(1);
		}
	}
}
