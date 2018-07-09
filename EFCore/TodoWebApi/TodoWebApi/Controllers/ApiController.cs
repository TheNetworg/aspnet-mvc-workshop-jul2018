using Microsoft.AspNetCore.Mvc;

namespace TodoWebApi.Controllers
{
    [Route("api/[controller]")]
    public abstract class ApiController : Controller
    {
        //add api/controller routing to all controllers
    }
}