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

        static TodoServiceInMemory()
        {
            //seeding
            Seed();
        }

        private static void Seed()
        {
            var names = new []
            {
                "Cleaning",
                "Cooking",
                "Dog walking",
                "Cat walking",
                "Lorem Ipsum",
            };

            var descs = new []
            {
                "Clean the hous",
                "Cook the dinned",
                "Walk the dog",
                "Walk the cat",
                "Lorem Ipsum dolor sit amet, nihil novus sub sole.",
            };

            var times = new[]
            {
                DateTime.Now,
                new DateTime(2018,7,8), 
                new DateTime(2018,5,5), 
                new DateTime(2018,10,10), 
                new DateTime(2018,11,11), 
            };

            for (int i = 0; i < 5; i++)
            {
                _data.Add(new TodoItem
                {
                    Id = GetUniqueId(),
                    Description = descs[i],
                    DueDate = times[i],
                    Name = names[i]
                });
            }
        }

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
                _data.Add(entity);
            }
            else
            {
                _data.Remove(_data.First(x => x.Id == dto.Id));
                _data.Add(entity);
            }            
        }

        /// <inheritdoc />
        public void Delete(int id)
        {
            _data.Remove(_data.First(x => x.Id == id));
        }
    }
}
