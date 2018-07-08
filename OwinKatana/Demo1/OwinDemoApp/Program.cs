using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin.Hosting;
using Owin;

namespace OwinDemoApp
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
            app.Use(async (context, next) =>
            {
                foreach (var pair in context.Environment)
                {
                    Console.WriteLine(pair);
                }

                await next();
            });

            app.Use(async (context, next) =>
            {
                Console.WriteLine($"Requesting: {context.Request.Path}");
                await next(); // remember writing this in hello world component? nope, try to shuffle component order

                //we can manipulate response after the next state
                Console.WriteLine($"Response: {context.Response.StatusCode}");
            });

            ConfigureWebAPI(app);            
            app.UseHelloWorld();
        }

        private void ConfigureWebAPI(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}");
            app.UseWebApi(config);
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
