using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using Owin;

namespace KatanaDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string uri = "http://localhost:9090";
            using (WebApp.Start<Startup>(uri))
            {
                Console.WriteLine("Started");
                Console.ReadKey();
            }
        }
    }

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseWelcomePage();

            app.Run(context =>
            {
                return context.Response.WriteAsync("Hello");
            });
        }
    }
}
