using ApplicationCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure
{
    public interface ITodoRepository : IDisposable
    {
        Task<List<Todo>> GetAllAsync(CancellationToken ct = default(CancellationToken));
        Task<List<Todo>> GetByUserIdAsync(Guid id, CancellationToken ct = default(CancellationToken));
        Task<Todo> AddAsync(Todo todo, CancellationToken ct = default(CancellationToken));
        Task<bool> UpdateAsync(Todo todo, CancellationToken ct = default(CancellationToken));
        Task<bool> DeleteAsync(Guid id, CancellationToken ct = default(CancellationToken));
        Task<Todo> GetByIdAsync(Guid id, CancellationToken ct = default(CancellationToken));

    }
}
