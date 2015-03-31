using System.Collections.Generic;
using System.Linq;

namespace SignalrTesting.SignalR
{
    public class ConnectionMapping<T>
    {
        private readonly List<string> _connections =
            new List<string>();

        public int Count
        {
            get
            {
                return _connections.Count;
            }
        }

        public void Add(string connectionId)
        {
            lock (_connections)
            {
                if (_connections.All(x => x != connectionId))
                {
                    _connections.Add(connectionId);
                }
            }
        }

        public IEnumerable<string> GetConnections()
        {
            return _connections;
        }

        public void Remove(string connectionId)
        {
            lock (_connections)
            {
                if (_connections.Any(x => x == connectionId))
                {
                    _connections.Remove(connectionId);
                }
            }
        }
    }
}