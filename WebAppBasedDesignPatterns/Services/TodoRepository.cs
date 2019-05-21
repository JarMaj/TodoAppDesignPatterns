using ApplicationCore;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Web.Services
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _context;

        private async Task<bool> TodoExistsAsync(Guid id, CancellationToken ct = default(CancellationToken))
        {
            return await GetByUserIdAsync(id, ct) != null;
        }

        public async Task<Todo> GetByIdAsync(Guid id, CancellationToken ct = default(CancellationToken))
        {
            return await _context.Todos.FindAsync(id);
        }

        public TodoRepository(TodoContext context)
        {
            _context = context;
        }
        public async Task<Todo> AddAsync(Todo todo, CancellationToken ct = default(CancellationToken))
        {
            _context.Todos.Add(todo);
           await _context.SaveChangesAsync(ct);
            return todo;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default(CancellationToken))
        {
            if (!await TodoExistsAsync(id, ct))
                return false;
            var toRemove = _context.Todos.Find(id);
            _context.Todos.Remove(toRemove);
           await _context.SaveChangesAsync(ct);
            return true;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<List<Todo>> GetAllAsync()
        {

            return await _context.Todos.ToListAsync();
        }

        public Task<List<Todo>> GetByUserIdAsync(Guid id, CancellationToken ct = default(CancellationToken))
        {
            return _context.Todos.Where(a => a.UserID == id).ToListAsync(ct);

        }



        public async Task<bool> UpdateAsync(Todo todo, CancellationToken ct = default(CancellationToken))
        {
            if ( !await TodoExistsAsync(todo.TodoID, ct))
                return false;
            _context.Todos.Update(todo);

            _context.Update(todo);
           await _context.SaveChangesAsync(ct);
            return true;
        }
    }
}
