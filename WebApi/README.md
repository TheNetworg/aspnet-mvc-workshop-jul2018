# Web API Demo
* Build a web API for a simple in memory todo list using Microsoft Web Api
* You are provided with a simple TODO Service in the backend
* Build simple CRUD Api that will call this service and modify in memory data
* In next tutorial we will add some database and made the changes persistent

## Steps
1. Register TODO service for dependency injection and inject it into controller
2. Create simple API definition that only calls the service and passes the dtos
3. (Optional) Create rest api definition using swashbuckle
4. [You're done](https://d1u5p3l4wpay3k.cloudfront.net/battlerite_gamepedia_en/c/cf/VO_Vanguard_Ultimate_8.mp3)

## Hints
```cs
services.AddScoped<IInterface, Implmentation>();     

[HttpGet]
[ProducesResponseType(typeof(IEnumerable<TodoItemDTO>), 200)]
public IActionResult  Get()
{
    return Ok(_service.GetAll());
}
```