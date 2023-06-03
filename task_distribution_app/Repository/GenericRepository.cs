using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using System.Web;
using task_distribution_app.Models.DTO;

namespace task_distribution_app.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private TaskDistributionEntities _context;
        private DbSet<T> _dbSet;
        private bool _disposed = false;

        public GenericRepository(TaskDistributionEntities context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Delete(object entityId)
        {
            T entityToDelete = _dbSet.Find(entityId);
            Delete(entityToDelete);
        }

        public void Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public T FindById(object entityId)
        {
            return _dbSet.Find(entityId);
        }

        public T FindByLambda(Expression<Func<T, bool>> filter = null)
        {
            if (filter != null)
                return _dbSet.FirstOrDefault(filter);
            else
                return null;
        }

        public T Insert(T entity)
        {
            return _dbSet.Add(entity);
        }

        public IEnumerable<T> Select(Expression<Func<T, bool>> filter = null)
        {
            if (filter != null)
                return _dbSet.Where(filter);
            else
                return _dbSet;
        }

        public T Update(T entity)
        {
            T updatedEntity = _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return updatedEntity;
        }

        public void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            using (TransactionScope tScope = new TransactionScope())
            {
                _context.SaveChanges();
                tScope.Complete();
            }
        }
    }
}