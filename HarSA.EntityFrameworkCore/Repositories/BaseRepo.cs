using HarSA.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace HarSA.EntityFrameworkCore.Repositories
{
    public class BaseRepo<T> : IRepo<T> where T : BaseEntity, new()
    {
        /// <summary>
        ///
        /// </summary>
        protected readonly HarDbContext Db;

        private readonly bool _disposeContext;
        private IDbContextTransaction _transaction;

        /// <summary>
        ///
        /// </summary>
        protected DbSet<T> Table;

        /// <summary>
        ///
        /// </summary>
        public HarDbContext Context => Db;

        /// <summary>
        ///
        /// </summary>
        public virtual IQueryable<T> QueryTable
        {
            get { return Table; }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="options"></param>
        public BaseRepo(DbContextOptions<HarDbContext> options) : this(new HarDbContext(options))
        {
            _disposeContext = true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        protected BaseRepo(HarDbContext context)
        {
            Db = context;
            Table = Db.Set<T>();
        }

        /// <summary>
        ///
        /// </summary>
        public int Count => Table.Count();

        /// <summary>
        ///
        /// </summary>
        public bool HasChanges => Db.ChangeTracker.HasChanges();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public bool Any() => Table.Any();

        /// <summary>
        ///
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public bool Any(Expression<Func<T, bool>> where) => Table.Any(where);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAll() => Table;

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TSortField"></typeparam>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        public IEnumerable<T> GetTop100<TSortField>(Expression<Func<T, TSortField>> orderBy, bool ascending)
            => ascending ? Table.OrderBy(orderBy).Take(100) : Table.OrderByDescending(orderBy).Take(100);

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TIncludeField"></typeparam>
        /// <param name="include"></param>
        /// <returns></returns>
        public IEnumerable<T> GetAll<TIncludeField>(Expression<Func<T, TIncludeField>> include)
            => Table.Include(include);

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TSortField"></typeparam>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        public IEnumerable<T> GetAll<TSortField>(Expression<Func<T, TSortField>> orderBy, bool ascending)
            => ascending ? Table.OrderBy(orderBy) : Table.OrderByDescending(orderBy);

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TIncludeField"></typeparam>
        /// <typeparam name="TSortField"></typeparam>
        /// <param name="include"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        public IEnumerable<T> GetAll<TIncludeField, TSortField>(
            Expression<Func<T, TIncludeField>> include, Expression<Func<T, TSortField>> orderBy, bool ascending)
            => ascending ? Table.Include(include).OrderBy(orderBy) : Table.Include(include).OrderByDescending(orderBy);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public T First() => Table.FirstOrDefault();

        /// <summary>
        ///
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public T First(Expression<Func<T, bool>> where) => Table.FirstOrDefault(where);

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TIncludeField"></typeparam>
        /// <param name="where"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        public T First<TIncludeField>(Expression<Func<T, bool>> where, Expression<Func<T, TIncludeField>> include)
            => Table.Where(where).Include(include).FirstOrDefault();

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Find(int id) => Table.Find(id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public T Find(Expression<Func<T, bool>> where)
            => Table.Where(where).FirstOrDefault();

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TIncludeField"></typeparam>
        /// <param name="where"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        public T Find<TIncludeField>(Expression<Func<T, bool>> where,
            Expression<Func<T, TIncludeField>> include)
            => Table.Where(@where).Include(include).FirstOrDefault();

        /// <summary>
        ///
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public IEnumerable<T> GetSome(Expression<Func<T, bool>> where)
            => Table.Where(where);

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TIncludeField"></typeparam>
        /// <param name="where"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        public IEnumerable<T> GetSome<TIncludeField>(Expression<Func<T, bool>> where,
            Expression<Func<T, TIncludeField>> include)
            => Table.Where(where).Include(include);

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TSortField"></typeparam>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        public IEnumerable<T> GetSome<TSortField>(
            Expression<Func<T, bool>> where, Expression<Func<T, TSortField>> orderBy, bool ascending)
            => ascending ? Table.Where(where).OrderBy(orderBy) : Table.Where(where).OrderByDescending(orderBy);

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
        public IEnumerable<T> GetSome<TIncludeField, TSortField>(
            Expression<Func<T, bool>> where, Expression<Func<T, TIncludeField>> include,
            Expression<Func<T, TSortField>> orderBy, bool ascending)
            => ascending ?
                Table.Where(where).OrderBy(orderBy).Include(include) :
                Table.Where(where).OrderByDescending(orderBy).Include(include);

        /// <summary>
        ///
        /// </summary>
        /// <param name="sqlString"></param>
        /// <returns></returns>
        public IEnumerable<T> FromSql(string sqlString)
            => Table.FromSql(sqlString);

        /// <summary>
        ///
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetRange(int skip, int take)
            => GetRange(Table, skip, take);

        /// <summary>
        ///
        /// </summary>
        /// <param name="query"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public IEnumerable<T> GetRange(IQueryable<T> query, int skip, int take)
            => query.Skip(skip).Take(take);

        /// <summary>
        ///
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="persist"></param>
        /// <returns></returns>
        public virtual int Add(T entity, bool persist = true)
        {
            Table.Add(entity);
            return persist ? SaveChanges() : 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="persist"></param>
        /// <returns></returns>
        public virtual int AddReturnEntityId(T entity, bool persist = true)
        {
            Table.Add(entity);
            SaveChanges();
            return entity.Id;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="persist"></param>
        /// <returns></returns>
        public virtual int AddRange(IEnumerable<T> entities, bool persist = true)
        {
            Table.AddRange(entities);
            return persist ? SaveChanges() : 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="persist"></param>
        /// <returns></returns>
        public virtual int Update(T entity, bool persist = true)
        {
            Table.Update(entity);
            return persist ? SaveChanges() : 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="persist"></param>
        /// <returns></returns>
        public virtual int UpdateRange(IEnumerable<T> entities, bool persist = true)
        {
            Table.UpdateRange(entities);
            return persist ? SaveChanges() : 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="persist"></param>
        /// <returns></returns>
        public virtual int Delete(T entity, bool persist = true)
        {
            Table.Remove(entity);
            return persist ? SaveChanges() : 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="persist"></param>
        /// <returns></returns>
        public virtual int DeleteRange(IEnumerable<T> entities, bool persist = true)
        {
            Table.RemoveRange(entities);
            return persist ? SaveChanges() : 0;
        }

        internal T GetEntryFromChangeTracker(int? id)
        {
            return Db.ChangeTracker.Entries<T>()
                .Select((EntityEntry e) => (T)e.Entity)
                    .FirstOrDefault(x => x.Id == id);
        }

        //TODO: Check For Cascade Delete
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="timeStamp"></param>
        /// <param name="persist"></param>
        /// <returns></returns>
        public int Delete(int id, byte[] timeStamp, bool persist = true)
        {
            var entry = GetEntryFromChangeTracker(id);
            if (entry != null)
            {
                if (timeStamp != null && entry.Timestamp.SequenceEqual(timeStamp))
                {
                    return Delete(entry, persist);
                }
                throw new Exception("Unable to delete due to concurrency violation.");
            }
            Db.Entry(new T { Id = id, Timestamp = timeStamp }).State = EntityState.Deleted;
            return persist ? SaveChanges() : 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            try
            {
                return Db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                //A concurrency error occurred
                //Should handle intelligently
                Console.WriteLine(ex);
                throw;
            }
            catch (RetryLimitExceededException ex)
            {
                //DbResiliency retry limit exceeded
                //Should handle intelligently
                Console.WriteLine(ex);
                throw;
            }
            catch (Exception ex)
            {
                //Should handle intelligently
                Console.WriteLine(ex);
                throw;
            }
        }

        public void BeginTransaction()
        {
            _transaction = Context.Database.BeginTransaction(IsolationLevel.RepeatableRead);
        }

        public void CommitTransaction()
        {
            _transaction.Commit();
        }

        public void RollbackTransaction()
        {
            _transaction.Rollback();
        }

        private bool _disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }
            if (_disposeContext)
            {
                Db.Dispose();
            }
            _disposed = true;
        }
    }
}
