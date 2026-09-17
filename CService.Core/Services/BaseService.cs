using System.Linq.Expressions;
using CService.Core.Entities;
using CService.Core.Interfaces;

namespace CService.Core.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly ICurrentUserService CurrentUserService;

        public BaseService(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            UnitOfWork = unitOfWork;
            CurrentUserService = currentUserService;
        }

        public Task<TEntity?> GetByIdAsync(int id) =>
            UnitOfWork.Repository<TEntity>().GetByIdAsync(id);

        public Task<IReadOnlyList<TEntity>> GetAllAsync() =>
            UnitOfWork.Repository<TEntity>().GetAllAsync();

        public Task<IReadOnlyList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate) =>
            UnitOfWork.Repository<TEntity>().FindAsync(predicate);

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            entity.CreatedDate = DateTime.UtcNow;
            entity.CreatedBy = CurrentUserService.UserId;

            await UnitOfWork.Repository<TEntity>().AddAsync(entity);
            await UnitOfWork.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            entity.UpdatedDate = DateTime.UtcNow;
            entity.UpdatedBy = CurrentUserService.UserId;

            UnitOfWork.Repository<TEntity>().Update(entity);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await UnitOfWork.Repository<TEntity>().GetByIdAsync(id);
            if (entity is null) return;

            entity.IsDeleted = true;
            entity.DeletedDate = DateTime.UtcNow;
            entity.DeletedBy = CurrentUserService.UserId;

            UnitOfWork.Repository<TEntity>().Update(entity);
            await UnitOfWork.SaveChangesAsync();
        }
        public IQueryable<TEntity> Query() => UnitOfWork.Repository<TEntity>().Query();
    }
}