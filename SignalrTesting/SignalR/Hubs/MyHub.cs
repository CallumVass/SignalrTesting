using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace SignalrTesting.SignalR.Hubs
{
    public class MyHub : Hub
    {
        private readonly static ConnectionMapping<string> Connections =
            new ConnectionMapping<string>();

        public void Send()
        {
            Clients.All.sendData(DateTime.Now);
        }

        public override Task OnConnected()
        {
            Connections.Add(Context.ConnectionId);

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            Connections.Remove(Context.ConnectionId);

            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            if (!Connections.GetConnections().Contains(Context.ConnectionId))
            {
                Connections.Add(Context.ConnectionId);
            }

            return base.OnReconnected();
        }
    }
}