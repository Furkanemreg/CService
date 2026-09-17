using System.Collections.Concurrent;
using CService.Core.Data;
using CService.Core.Entities;
using CService.Core.Interfaces;

namespace CService.Core.Factories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly ConcurrentDictionary<Type, object> _repositories = new();

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            return (IRepository<TEntity>)_repositories.GetOrAdd(
                typeof(TEntity),
                _ => new Repository<TEntity>(_context));
        }

        public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
