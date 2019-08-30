using HarSA.Domain;
using HarSA.EntityFrameworkCore.Repositories;
using HarSA.Infrastructure;
using System;
using System.Linq;

namespace HarSA.EntityFrameworkCore.Application
{
    public class CrudService<TEntity> : ICrudService<TEntity> where TEntity : BaseEntity, new()
    {
        protected IRepo<TEntity> Repository { get; }

        public CrudService(IRepo<TEntity> repo)
        {
            Repository = repo;
        }

        public int Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return Repository.Add(entity);
        }

        public int Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            return Repository.Delete(entity);
        }

        public TEntity Get(int id)
        {
            return Repository.Find(id);
        }

        public IPagedList<TEntity> GetAll(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var entities = Repository.GetAll();
            return new PagedList<TEntity>(entities.ToList(), pageIndex, pageSize);
        }

        public int Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return Repository.Update(entity);
        }
    }
}
