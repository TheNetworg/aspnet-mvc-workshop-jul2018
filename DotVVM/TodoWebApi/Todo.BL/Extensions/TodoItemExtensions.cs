using Todo.BL.DTO;
using Todo.DAL.Entities;

namespace Todo.BL.Extensions
{
    public static class TodoItemExtensions
    {
        public static TodoItemDTO ToDto(this TodoItem item)
        {
            return new TodoItemDTO
            {
                Description = item.Description,
                DueDate = item.DueDate,
                Name = item.Name,
                Id = item.Id
            };
        }

        internal static TodoItem ToEntity(this TodoItemDTO item)
        {
            return new TodoItem
            {
                Description = item.Description,
                DueDate = item.DueDate,
                Name = item.Name,
                Id = item.Id
            };
        }
    }
}