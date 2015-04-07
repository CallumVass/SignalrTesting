using System;
using System.Threading;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Nancy.Owin;
using Owin;
using SignalrTesting.Nancy;
using SignalrTesting.SignalR;

namespace SignalrTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            const string url = "http://localhost:10000";
            using (WebApp.Start(url))
            {
                Console.WriteLine("Server running on {0}", url);

                var hubConnection = new HubConnection(url);
                var hubProxy = hubConnection.CreateHubProxy("MyHub");

                hubConnection.Start().ContinueWith(task =>
                {
                    if (!task.IsFaulted)
                        Console.WriteLine("Connected");
                    else
                        Console.WriteLine(task.Exception.GetBaseException());
                }).Wait();

                var timer = new Timer(x =>
                {
                    if (ConnectionMapping.Count <= 1) return;

                    Console.WriteLine("Connections: {0}", ConnectionMapping.Count);
                    hubProxy.Invoke("Send").Wait();
                }, null, 0, 2000);

                Console.ReadLine();
            }
        }
    }

    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app
                .UseCors(CorsOptions.AllowAll)
                .MapSignalR()
                .UseNancy(Configuration);
        }

        private static void Configuration(NancyOptions nancyOptions)
        {
            nancyOptions.Bootstrapper = new Bootstrapper();
        }
    }
}
