using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace RwdApp
{
	public class TimeBridge
	{
		private Timer _timer;

		public TimeBridge()
		{
			_timer = new Timer();
			_timer.Interval = 1000;
			_timer.Tick += _timer_Tick;
			_timer.Start();
		}

		public event Action<TimeBridgeEventArg> TimeUpdated;

		private void _timer_Tick(object sender, EventArgs e)
		{
			TimeUpdated(new TimeBridgeEventArg() { Time = DateTime.Now.ToString("HH:mm:ss") });
		}
	}

	public class TimeBridgeEventArg
	{
		public string Time { get; set; }
	}
}
