using System.Web.Http;

namespace OwinDemoApp
{
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
}