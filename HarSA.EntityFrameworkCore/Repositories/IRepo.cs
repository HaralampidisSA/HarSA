using HarSA.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HarSA.EntityFrameworkCore.Repositories
{
    public interface IRepo<T> : IDisposable where T : BaseEntity, new()
    {
        HarDbContext Context { get; }

        IQueryable<T> QueryTable { get; }

        /// <summary>
        /// Gets the number of records.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Gets if the istance has any changes.
        /// </summary>
        bool HasChanges { get; }

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

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TSortField"></typeparam>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        IEnumerable<T> GetSome<TSortField>(
            Expression<Func<T, bool>> where, Expression<Func<T, TSortField>> orderBy, bool ascending);

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TIncludeField"></typeparam>
        /// <typeparam name="TSortField"></typeparam>
        /// <param name="where"></param>
        /// <param name="include"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        IEnumerable<T> GetSome<TIncludeField, TSortField>(
            Expression<Func<T, bool>> where, Expression<Func<T, TIncludeField>> include,
            Expression<Func<T, TSortField>> orderBy, bool ascending = true);

        /// <summary>
        ///
        /// </summary>
        /// <param name="sqlString"></param>
        /// <returns></returns>
        IEnumerable<T> FromSql(string sqlString);

        /// <summary>
        ///
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        IEnumerable<T> GetRange(int skip, int take);

        /// <summary>
        ///
        /// </summary>
        /// <param name="query"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        IEnumerable<T> GetRange(IQueryable<T> query, int skip, int take);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        T First();

        /// <summary>
        ///
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        T First(Expression<Func<T, bool>> where);

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TIncludeField"></typeparam>
        /// <param name="where"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        T First<TIncludeField>(Expression<Func<T, bool>> where, Expression<Func<T, TIncludeField>> include);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Find(int id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        T Find(Expression<Func<T, bool>> where);

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TIncludeField"></typeparam>
        /// <param name="where"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        T Find<TIncludeField>(
            Expression<Func<T, bool>> where,
            Expression<Func<T, TIncludeField>> include);

        /// <summary>
        ///
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="persist"></param>
        /// <returns></returns>
        int Add(T entity, bool persist = true);

        /// <summary>
        ///
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="persist"></param>
        /// <returns></returns>
        int AddReturnEntityId(T entity, bool persist = true);

        /// <summary>
        ///
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="persist"></param>
        /// <returns></returns>
        int AddRange(IEnumerable<T> entities, bool persist = true);

        /// <summary>
        ///
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="persist"></param>
        /// <returns></returns>
        int Update(T entity, bool persist = true);

        /// <summary>
        ///
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="persist"></param>
        /// <returns></returns>
        int UpdateRange(IEnumerable<T> entities, bool persist = true);

        /// <summary>
        ///
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="persist"></param>
        /// <returns></returns>
        int Delete(T entity, bool persist = true);

        /// <summary>
        ///
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="persist"></param>
        /// <returns></returns>
        int DeleteRange(IEnumerable<T> entities, bool persist = true);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="timeStamp"></param>
        /// <param name="persist"></param>
        /// <returns></returns>
        int Delete(int id, byte[] timeStamp, bool persist = true);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        ///
        /// </summary>
        void BeginTransaction();

        /// <summary>
        ///
        /// </summary>
        void CommitTransaction();

        /// <summary>
        ///
        /// </summary>
        void RollbackTransaction();
    }
}
