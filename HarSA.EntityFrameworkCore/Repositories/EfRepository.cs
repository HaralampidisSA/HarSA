using HarSA.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HarSA.EntityFrameworkCore.Repositories
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private readonly IDbContext Db;
        private readonly bool _disposeContext;
        protected DbSet<T> Table;

        public IDbContext Context => Db;

        public IQueryable<T> QueryTable => Table;

        public EfRepository(IDbContext dbContext)
        {
            _disposeContext = true;
            Db = dbContext;
            Table = Db.Set<T>();
        }

        public int Count => Table.Count();

        public int Add(T entity, bool persist = true)
        {
            Table.Add(entity);
            return persist ? SaveChanges() : 0;
        }

        public int AddRange(IEnumerable<T> entities, bool persist = true)
        {
            Table.AddRange(entities);
            return persist ? SaveChanges() : 0;
        }

        public bool Any()
        {
            return Table.Any();
        }

        public bool Any(Expression<Func<T, bool>> where)
        {
            return Table.Any(where);
        }

        public int Delete(T entity, bool persist = true)
        {
            Table.Remove(entity);
            return persist ? SaveChanges() : 0;
        }

        public int DeleteRange(IEnumerable<T> entities, bool persist = true)
        {
            Table.RemoveRange(entities);
            return persist ? SaveChanges() : 0;
        }

        public T Find(int id) => Table.Find(id);

        public T Find(Expression<Func<T, bool>> where)
            => Table.Where(where).FirstOrDefault();

        public T Find<TIncludeField>(Expression<Func<T, bool>> where,
            Expression<Func<T, TIncludeField>> include)
            => Table.Where(@where).Include(include).FirstOrDefault();

        public T First() => Table.FirstOrDefault();

        public T First(Expression<Func<T, bool>> where) => Table.FirstOrDefault(where);

        public T First<TIncludeField>(Expression<Func<T, bool>> where, Expression<Func<T, TIncludeField>> include)
            => Table.Where(where).Include(include).FirstOrDefault();

        public IEnumerable<T> FromSql(string sqlString)
            => Table.FromSqlRaw(sqlString);

        public virtual IEnumerable<T> GetAll() => Table;

        public IEnumerable<T> GetAll<TIncludeField>(Expression<Func<T, TIncludeField>> include)
            => Table.Include(include);

        public IEnumerable<T> GetAll<TSortField>(Expression<Func<T, TSortField>> orderBy, bool ascending)
            => ascending ? Table.OrderBy(orderBy) : Table.OrderByDescending(orderBy);

        public IEnumerable<T> GetAll<TIncludeField, TSortField>(
            Expression<Func<T, TIncludeField>> include, Expression<Func<T, TSortField>> orderBy, bool ascending)
            => ascending ? Table.Include(include).OrderBy(orderBy) : Table.Include(include).OrderByDescending(orderBy);

        public virtual IEnumerable<T> GetRange(int skip, int take)
            => GetRange(Table, skip, take);

        public IEnumerable<T> GetRange(IQueryable<T> query, int skip, int take)
            => query.Skip(skip).Take(take);

        public IEnumerable<T> GetSome(Expression<Func<T, bool>> where)
            => Table.Where(where);

        public IEnumerable<T> GetSome<TIncludeField>(Expression<Func<T, bool>> where,
            Expression<Func<T, TIncludeField>> include)
            => Table.Where(where).Include(include);

        public IEnumerable<T> GetSome<TSortField>(
            Expression<Func<T, bool>> where, Expression<Func<T, TSortField>> orderBy, bool ascending)
            => ascending ? Table.Where(where).OrderBy(orderBy) : Table.Where(where).OrderByDescending(orderBy);

        public IEnumerable<T> GetSome<TIncludeField, TSortField>(
            Expression<Func<T, bool>> where, Expression<Func<T, TIncludeField>> include,
            Expression<Func<T, TSortField>> orderBy, bool ascending)
            => ascending ?
                Table.Where(where).OrderBy(orderBy).Include(include) :
                Table.Where(where).OrderByDescending(orderBy).Include(include);

        public IEnumerable<T> GetTop100<TSortField>(Expression<Func<T, TSortField>> orderBy, bool ascending)
            => ascending ? Table.OrderBy(orderBy).Take(100) : Table.OrderByDescending(orderBy).Take(100);

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

        public virtual int Update(T entity, bool persist = true)
        {
            Table.Update(entity);
            return persist ? SaveChanges() : 0;
        }

        public virtual int UpdateRange(IEnumerable<T> entities, bool persist = true)
        {
            Table.UpdateRange(entities);
            return persist ? SaveChanges() : 0;
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
