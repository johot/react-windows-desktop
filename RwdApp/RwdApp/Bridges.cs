using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RwdApp
{
	public class Bridges
	{
		public IEnumerable<object> GetBridges()
		{
			return new List<object>()
			{
				new FileBridge(),
				new DialogBridge(),
				new TimeBridge()
			};
		}
	}
}
