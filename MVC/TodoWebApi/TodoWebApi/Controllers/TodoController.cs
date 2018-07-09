using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.BL;
using Todo.BL.DTO;

namespace TodoWebApi.Controllers
{
    [Authorize]
    public class TodoController : ApiController
    {
        private readonly ITodoService _service;

        public TodoController(ITodoService service)
        {
            _service = service;
        }

        // GET api/values
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TodoItemDTO>), 200)]
        public IActionResult  Get()
        {
            return Ok(_service.GetAll());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TodoItemDTO), 200)]
        public IActionResult Get(int id)
        {
            return Ok(_service.GetById(id));
        }

        // POST api/values
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Update([FromBody]TodoItemDTO value)
        {
            _service.Save(value);
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok();            
        }
    }
}
