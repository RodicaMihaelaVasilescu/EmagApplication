using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;

namespace WebApplication.Hub
{
	public class ItemHub : Microsoft.AspNet.SignalR.Hub
	{
		public void Send(string name, string message)
		{
			// Call the broadcastMessage method to update clients.
			Clients.All.broadcastMessage(name, message);
		}

		public void SendAsync(HttpRequestMessage response)
		{
			// Call the broadcastMessage method to update clients.
			Clients.All.broadcastMessage(response);
		}

		public async Task AddNewItems(Product product)
		{
			await Clients.All.addNewMessageToPage("", "");
		}
		
		public async Task NewItemMessage(string name, string message)
		{
			await Clients.All.addNewMessageToPage(name, message);
		}
		public override Task OnConnected()
		{
			// Add your own code here.
			// For example: in a chat application, record the association between
			// the current connection ID and user name, and mark the user as online.
			// After the code in this method completes, the client is informed that
			// the connection is established; for example, in a JavaScript client,
			// the start().done callback is executed.
			return base.OnConnected();
		}

		public override Task OnDisconnected(bool stopCalled)
		{
			// Add your own code here.
			// For example: in a chat application, mark the user as offline, 
			// delete the association between the current connection id and user name.
			return base.OnDisconnected(stopCalled);
		}

		public override Task OnReconnected()
		{
			// Add your own code here.
			// For example: in a chat application, you might have marked the
			// user as offline after a period of inactivity; in that case 
			// mark the user as online again.
			return base.OnReconnected();
		}
    }
}
