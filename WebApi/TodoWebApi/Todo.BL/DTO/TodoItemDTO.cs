using System;

namespace Todo.BL.DTO
{
    public class TodoItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }        
        public DateTime DueDate { get; set; }  
    }
}