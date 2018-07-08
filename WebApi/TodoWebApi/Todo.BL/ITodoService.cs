using System.Collections.Generic;
using Todo.BL.DTO;

namespace Todo.BL
{
    public interface ITodoService
    {
        /// <summary>
        /// Create new TodoItem
        /// </summary>
        /// <returns></returns>
        TodoItemDTO Create();
        /// <summary>
        /// Delete TodoItem with given id
        /// </summary>
        /// <param name="id">Id to be deleted</param>
        void Delete(int id);
        /// <summary>
        /// Get all ids
        /// </summary>
        /// <param name="pageSize">Page size, only this is the maximum number to be rturned</param>
        /// <param name="skip">How many should be skipped</param>
        /// <returns>IEnumerable of all items</returns>
        IEnumerable<TodoItemDTO> GetAll(int pageSize = int.MaxValue, int skip = 0);
        /// <summary>
        /// Find TodoItem by id
        /// </summary>
        /// <param name="id">Id to be found</param>
        /// <returns>Insance of TodoItem with given id</returns>
        TodoItemDTO GetById(int id);
        /// <summary>
        /// Save current DTO, if the id is 0 (new instance) then it will be created, otherwise the data will be updated
        /// </summary>
        /// <param name="dto">DTO to be saved</param>
        void Save(TodoItemDTO dto);
    }
}