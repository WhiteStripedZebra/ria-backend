using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Engineer.Domain.Entities;
using Engineer.Domain.Repositories;
using Engineer.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Engineer.Application.Repository.Tasks
{
    public class ToDoRepository : ITodoRepository
    {
        private readonly ApplicationDbContext _context;

        public ToDoRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<ToDo>> GetTasksAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<ToDo> GetTaskAsync(Guid id)
        {
            return await _context.Tasks.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public void AddToDo(ToDo entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            entity.CreatedAt = DateTimeOffset.Now;

            _context.Add(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}