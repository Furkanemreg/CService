using System.Linq.Expressions;
using CService.Core.Entities;
using CService.Core.Factories;
using CService.Core.Interfaces;

namespace CService.Core.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        protected readonly IUnitOfWork UnitOfWork;

        public BaseService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public Task<TEntity?> GetByIdAsync(int id) =>
            UnitOfWork.Repository<TEntity>().GetByIdAsync(id);

        public Task<IReadOnlyList<TEntity>> GetAllAsync() =>
            UnitOfWork.Repository<TEntity>().GetAllAsync();

        public Task<IReadOnlyList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate) =>
            UnitOfWork.Repository<TEntity>().FindAsync(predicate);

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await UnitOfWork.Repository<TEntity>().AddAsync(entity);
            await UnitOfWork.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            UnitOfWork.Repository<TEntity>().Update(entity);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await UnitOfWork.Repository<TEntity>().GetByIdAsync(id);
            if (entity is null) return;

            UnitOfWork.Repository<TEntity>().Remove(entity);
            await UnitOfWork.SaveChangesAsync();
        }
    }
}
