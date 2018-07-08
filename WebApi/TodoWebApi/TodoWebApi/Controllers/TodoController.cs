using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Todo.BL.DTO;

namespace TodoWebApi.Controllers
{
    public class TodoController : ApiController
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<TodoItemDTO> Get()
        {
            throw new NotImplementedException();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public TodoItemDTO Get(int id)
        {
           throw new NotImplementedException();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]TodoItemDTO value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
