using System;
using System.Collections.Generic;
using System.Linq;
using Todo.BL.DTO;
using Todo.BL.Extensions;
using Todo.DAL.Entities;

namespace Todo.BL
{    
    public class TodoServiceInMemory : ITodoService
    {
        private static readonly List<TodoItem> _data = new List<TodoItem>();
        private static int _lastId = 0;

        private static int GetUniqueId()
        {
            return _lastId++;
        }

        /// <inheritdoc />
        public TodoItemDTO GetById(int id)
        {
            return _data.FirstOrDefault(x => x.Id == id).ToDto();
        }

        /// <inheritdoc />
        public IEnumerable<TodoItemDTO> GetAll(int pageSize = int.MaxValue, int skip = 0)
        {
            return _data
                .Skip(skip)
                .Take(pageSize)
                .Select(x => x.ToDto());
        }

        /// <inheritdoc />
        public TodoItemDTO Create()
        {
            return new TodoItemDTO();
        }

        /// <inheritdoc />
        public void Save(TodoItemDTO dto)
        {
            var entity = dto.ToEntity();
            //if new
            if (entity.Id == 0)
            {
                entity.Id = GetUniqueId();
            }
            _data.Add(entity);
        }

        /// <inheritdoc />
        public void Delete(int id)
        {
            _data.Remove(_data.First(x => x.Id == id));
        }
    }
}
