using System;

namespace Todo.BL.DTO
{
    public class TodoItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }        
        public DateTime DueDate { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Description)}: {Description}, {nameof(DueDate)}: {DueDate}";
        }
    }
}