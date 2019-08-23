using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Engineer.Domain.Entities;

namespace Engineer.Domain.Repositories
{
    public interface ITodoRepository
    {
        Task<IEnumerable<ToDo>> GetTasksAsync();

        Task<ToDo> GetTaskAsync(Guid id);

        void AddToDo(ToDo entity);

        Task<bool> SaveChangesAsync();

        Task<bool> UpdateTask(ToDo entity);

        Task<bool> DeleteTask(Guid id);
    }
}