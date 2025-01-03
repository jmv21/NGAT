using NGAT.Business.Contracts.DataAccess;
using NGAT.Business.Contracts.Services;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace NGAT.Services.ApplicationServices
{
    public class ApplicationService<TEntity> : IApplicationService<TEntity> where TEntity : class
    {
        public ApplicationService(IRepository<TEntity> repository)
        {
            this.Repository = repository;
        }

        protected virtual IRepository<TEntity> Repository { get; set; }

        #region CRUD
        public TEntity Add(TEntity entity)
        {
            return Repository.Add(entity);
        }

        public TEntity Delete(TEntity entity)
        {
            return Repository.Delete(entity);
        }

        public TEntity Find(params object[] key)
        {
            return Repository.Find(key);
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            return Repository.GetAll(filter, propertySelectors);
        }

        public TEntity Update(TEntity entity)
        {
            return Repository.Edit(entity);
        }
        #endregion

    }
}
