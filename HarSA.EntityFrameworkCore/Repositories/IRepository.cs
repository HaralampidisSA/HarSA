using HarSA.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HarSA.EntityFrameworkCore.Repositories
{
    public interface IRepository<T> : IDisposable where T : BaseEntity, new()
    {
        IDbContext Context { get; }

        IQueryable<T> QueryTable { get; }

        /// <summary>
        /// Gets the number of records.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Gets if table has any records.
        /// </summary>
        /// <returns>True if it has; Otherwise false.</returns>
        bool Any();

        /// <summary>
        /// Gets if table has any records with filter for any entity property.
        /// </summary>
        /// <param name="where">Entity property filter.</param>
        /// <returns>True if it has; Otherwise false.</returns>
        bool Any(Expression<Func<T, bool>> where);

        /// <summary>
        /// Gets all the records of the entity in a <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/> where T is the entity.</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Gets top 100 records of the entity sorted by <c>TSortField</c>.
        /// </summary>
        /// <typeparam name="TSortField"></typeparam>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        IEnumerable<T> GetTop100<TSortField>(Expression<Func<T, TSortField>> orderBy, bool ascending);

        /// <summary>
        /// Gets all the records of the entity in a <see cref="IEnumerable{T}"/>. Includes properties of mapped entities.
        /// </summary>
        /// <typeparam name="TIncludeField">The entity to include.</typeparam>
        /// <param name="include">The entity to include.</param>
        /// <returns><see cref="IEnumerable{T}"/> where T is the entity.</returns>
        IEnumerable<T> GetAll<TIncludeField>(Expression<Func<T, TIncludeField>> include);

        /// <summary>
        /// Gets all the records of the entity in a <see cref="IEnumerable{T}"/>. Sorted by the <c>TSortField</c>.
        /// </summary>
        /// <typeparam name="TSortField"></typeparam>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        IEnumerable<T> GetAll<TSortField>(Expression<Func<T, TSortField>> orderBy, bool ascending);

        /// <summary>
        /// Gets all the records of the entity in a <see cref="IEnumerable{T}"/>. Includes properties of mapped entities and sorted by the <c>TSortField</c>.
        /// </summary>
        /// <typeparam name="TIncludeField"></typeparam>
        /// <typeparam name="TSortField"></typeparam>
        /// <param name="include"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        IEnumerable<T> GetAll<TIncludeField, TSortField>(
            Expression<Func<T, TIncludeField>> include,
            Expression<Func<T, TSortField>> orderBy, bool ascending);

        /// <summary>
        /// Gets records of the entity in a <see cref="IEnumerable{T}"/> filtered by.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        IEnumerable<T> GetSome(Expression<Func<T, bool>> where);

        IEnumerable<T> GetSome<TIncludeField>(
            Expression<Func<T, bool>> where, Expression<Func<T, TIncludeField>> include);

        IEnumerable<T> GetSome<TSortField>(
            Expression<Func<T, bool>> where, Expression<Func<T, TSortField>> orderBy, bool ascending);

        IEnumerable<T> GetSome<TIncludeField, TSortField>(
            Expression<Func<T, bool>> where, Expression<Func<T, TIncludeField>> include,
            Expression<Func<T, TSortField>> orderBy, bool ascending = true);

        IEnumerable<T> FromSql(string sqlString);

        IEnumerable<T> GetRange(int skip, int take);

        IEnumerable<T> GetRange(IQueryable<T> query, int skip, int take);

        T First();

        T First(Expression<Func<T, bool>> where);

        T First<TIncludeField>(Expression<Func<T, bool>> where, Expression<Func<T, TIncludeField>> include);

        T Find(int id);

        T Find(Expression<Func<T, bool>> where);

        T Find<TIncludeField>(
            Expression<Func<T, bool>> where,
            Expression<Func<T, TIncludeField>> include);

        int Add(T entity, bool persist = true);

        int AddRange(IEnumerable<T> entities, bool persist = true);

        int Update(T entity, bool persist = true);

        int UpdateRange(IEnumerable<T> entities, bool persist = true);

        int Delete(T entity, bool persist = true);

        int DeleteRange(IEnumerable<T> entities, bool persist = true);

        int SaveChanges();
    }
}
