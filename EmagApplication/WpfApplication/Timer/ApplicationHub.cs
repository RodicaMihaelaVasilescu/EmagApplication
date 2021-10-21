using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.SignalR;
using WebApplication.Hub;
using WpfApplication.Timer;

public class ApplicationHub : Microsoft.AspNetCore.SignalR.Hub<ItemHub>
{
	// This method will be called on Start button click
	//public CustomTimer timer = new CustomTimer(1000);
	//public async Task StartTime()
	//{
	//	timer = CustomTimer.Timers.GetOrAdd(Context.ConnectionId, timer);
	//	timer.callerContext = Context;
	//	//timer.hubCallerClients = Clients; 
	//	////timer.hubCallerClients =  GlobalHost.ConnectionManager.GetHubContext<ItemHub>();
	//	timer.Elapsed += aTimer_Elapsed;
	//	timer.Interval = 1000;
	//	timer.Enabled = true;
	//}

	//// This method will pass time to all connected clients
	//void aTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
	//{
	//	var timer = (CustomTimer)sender;
	//	timer = (CustomTimer)sender;
	//	HubCallerContext hcallerContext = timer.callerContext;
	//	IHubCallerClients<IClient> hubClients = timer.hubCallerClients;

	//	//hubClients.Clients.All.ShowTime(DateTime.Now.Hour.ToString() +
	//	//								":" + DateTime.Now.Minute.ToString() + ":" +
	//	//								DateTime.Now.Second.ToString());
	//}

	//// This should stop running timer on button click event from client
	//public async Task StopTime()
	//{
	//	timer = CustomTimer.Timers.GetOrAdd(Context.ConnectionId, timer);
	//	timer.Elapsed -= aTimer_Elapsed;
	//	timer.Enabled = false;

	//	//await Clients.All.StopTime("Timer Stopped");
	//}
}