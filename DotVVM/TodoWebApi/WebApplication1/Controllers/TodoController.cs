using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.BL;
using Todo.BL.DTO;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        private readonly ITodoService _service;

        public TodoController(ITodoService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View(_service.GetAll());
        }

        public IActionResult Edit(int id)
        {
            return View(_service.GetById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Name,Description,DueDate")] TodoItemDTO dto)
        {
            dto.Id = id;
            _service.Save(dto);
            return RedirectToAction("Index", _service.GetAll());
        }

        public IActionResult Details(int id)
        {
            return View(_service.GetById(id));
        }

        public IActionResult Create()
        {
            return View("Edit", _service.Create());
        }

        public IActionResult Delete(int id)
        {
            return View(_service.GetById(id));
        }      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, TodoItemDTO dto)
        {
            _service.Delete(id);
            return RedirectToAction("Index", _service.GetAll());
        }      
    }
}