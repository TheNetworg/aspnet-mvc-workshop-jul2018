# Webserver from scratch
1. Empty console application in Visual Studio
## Nugets
```Powershell
Install-Package Microsoft.Owin.Hosting -Version 4.0.0
Install-Package Microsoft.Owin.Host.HttpListener -Version 4.0.0
```
2. Make a web server ☺
3. class Startup

```cs
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {            
        }
    }
```

4. Hello World
```cs
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
            app.Run(context =>
            {
                return context.Response.WriteAsync("Hello");
            });
        }
    }
```
5. Wellcome page
```Powershell
Install-Package Microsoft.Owin.Diagnostics -Version 4.0.0
```

```cs
    app.UseWelcomePage();
```

# Application Function
Make a component
```cs
        public class HelloWorldComponent
    {
        private readonly Func<IDictionary<string, object>, Task> _next;

        public HelloWorldComponent(Func<IDictionary<string, object>,Task> next)
        {
            _next = next;
        }

        public async Task Invoke(IDictionary<string, object> enviroment)
        {
            await _next(enviroment);
        }
    }
```
Manipulate raw OWIN objects
```cs
public Task Invoke(IDictionary<string, object> enviroment)
        {
            var response = enviroment["owin.ResponseBody"] as Stream;
            using (var writter = new StreamWriter(response))
            {
                return writter.WriteAsync("Hello World");                
            }
        }
```

Make it nice with etension method
```cs
public static class AppBuilderExtensions
    {
        public static void UseHelloWorld(this IAppBuilder app)
        {
            app.Use<HelloWorldComponent>();
        }
    }
    //...
    app.UseHelloWorld();
```

# Middleware
Let's write the enviroment dictionary to std out
```cs
app.Use(async (context, next) =>
            {
                foreach (var pair in context.Environment)
                {
                    Console.WriteLine(pair);
                }

                await next(); // process to next component in the pipeline
            });
```
# Application layer
```
Install-Package Microsoft.AspNet.WebApi.OwinSelfHost -Version 5.2.6
```
Configure web API
```cs
ConfigureWebAPI(app);            
app.UseHelloWorld();
// ...

        private void ConfigureWebAPI(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}");
            app.UseWebApi(config);
        }

//Model class
 public class Greeting
    {
        public string Text { get; set; }
    }


//Controller definition
public class GreetingController:ApiController
    {
        public Greeting Get()
        {
            return new Greeting {Text = $"Hello world"};
        }

        public Greeting Get(string name)
        {
            return new Greeting {Text = $"Hello {name}"};
        }
    }
```