using System;
using System.Linq;
using System.Linq.Expressions;

namespace NGAT.Business.Contracts.Services
{
    /// <summary>
    /// Represents an application service for the service layer
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IApplicationService<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets all entities in the repository
        /// </summary>
        /// <param name="filter">Filters the elements to retrieve</param>
        /// <param name="propertySelectors">Selects the (navigation) properties to include in the query</param>
        /// <returns>A list of selected elements</returns>
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] propertySelectors);

        #region CRUD
        TEntity Add(TEntity entity);

        TEntity Find(params object[] key);

        TEntity Update(TEntity entity);

        TEntity Delete(TEntity entity);
        #endregion
    }
}
