using System;
using System.Collections.Generic;
using System.Linq;
using Todo.BL.DTO;
using Todo.BL.Extensions;
using Todo.DAL;

namespace Todo.BL
{
    public class TodoServiceEF : ITodoService
    {
        private readonly AppDbContext _context;

        public TodoServiceEF(AppDbContext context)
        {
            _context = context;
        }

        public TodoItemDTO Create()
        {
            return new TodoItemDTO();
        }

        public void Delete(int id)
        {
            _context.Items.Remove(_context.Items.First(x => x.Id == id));
            _context.SaveChanges();
        }

        public IEnumerable<TodoItemDTO> GetAll(int pageSize = Int32.MaxValue, int skip = 0)
        {
            return _context.Items.Select(x => x.ToDto());
        }

        public TodoItemDTO GetById(int id)
        {
            return _context.Items.First(x => x.Id == id).ToDto();
        }

        public void Save(TodoItemDTO dto)
        {         
            
                var entity = dto.ToEntity();
                //if new
                if (entity.Id == 0)
                {
                    _context.Items.Add(entity);
                }
                else
                {
                    _context.Items.Update(entity);
                }

            _context.SaveChanges();
        }
    }
}