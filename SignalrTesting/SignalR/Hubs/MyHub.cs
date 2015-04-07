using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace SignalrTesting.SignalR.Hubs
{
    public class MyHub : Hub
    {
        public void Send()
        {
            Clients.All.sendData(DateTime.Now);
        }

        public override Task OnConnected()
        {
            ConnectionMapping.Add(Context.ConnectionId);

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            ConnectionMapping.Remove(Context.ConnectionId);

            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            if (!ConnectionMapping.GetConnections().Contains(Context.ConnectionId))
            {
                ConnectionMapping.Add(Context.ConnectionId);
            }

            return base.OnReconnected();
        }
    }
}