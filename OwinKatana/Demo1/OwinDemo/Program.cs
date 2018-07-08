using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using Owin;

namespace OwinDemo
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
            //app.Use<HelloWorldComponent>();
            app.UseHelloWorld();
        }
    }

    /// <summary>
    /// HelloWorld for evry request
    /// </summary>
    public class HelloWorldComponent
    {
        private readonly Func<IDictionary<string, object>, Task> _next;

        public HelloWorldComponent(Func<IDictionary<string, object>,Task> next)
        {
            _next = next;
        }

        public Task Invoke(IDictionary<string, object> enviroment)
        {
            var response = enviroment["owin.ResponseBody"] as Stream;
            using (var writter = new StreamWriter(response))
            {
                return writter.WriteAsync("Hello World");                
            }
        }
    }

    public static class AppBuilderExtensions
    {
        public static void UseHelloWorld(this IAppBuilder app)
        {
            app.Use<HelloWorldComponent>();
        }
    }
}
