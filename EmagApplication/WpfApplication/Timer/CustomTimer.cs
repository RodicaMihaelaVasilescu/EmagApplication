using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace WpfApplication.Timer
{
	public class CustomTimer : System.Timers.Timer
	{
		public CustomTimer(double interval)
			: base(interval)
		{
		}
		public static ConcurrentDictionary<string, CustomTimer> Timers = new ConcurrentDictionary<string, CustomTimer>();

		public HubCallerContext callerContext { get; set; }
		//public IHubCallerClients<IClient> hubCallerClients { get; set; }
	}
}
