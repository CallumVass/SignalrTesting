using System;
using System.Threading;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Nancy;
using Nancy.Owin;
using Owin;
using SignalrTesting.Nancy;

namespace SignalrTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            const string url = "http://+:10000";

            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Running on {0}", url);
                Console.WriteLine("Press enter to exit");

                var hubConnection = new HubConnection("http://localhost:10000");
                var hubProxy = hubConnection.CreateHubProxy("MyHub");

                hubConnection.Start().ContinueWith(task =>
                {
                    if (!task.IsFaulted)
                        Console.WriteLine("Connected");
                    else
                        Console.WriteLine(task.Exception.GetBaseException());
                }).Wait();

                hubProxy.On<DateTime>("sendData", x =>
                    Console.WriteLine(x));

                var timer = new Timer(x =>
                {
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
            app.UseCors(CorsOptions.AllowAll);
            app.UseNancy(Configuration)
                .MapSignalR();
        }

        private static void Configuration(NancyOptions nancyOptions)
        {
            nancyOptions.Bootstrapper = new Bootstrapper();
            nancyOptions.PerformPassThrough = context =>
                context.Response.StatusCode == HttpStatusCode.NotFound;
        }
    }
}
