using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace task_distribution_app.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        T FindById(object entityId);
        IEnumerable<T> Select(Expression<Func<T, bool>> filter = null);
        T Insert(T entity);
        T Update(T entity);
        T FindByLambda(Expression<Func<T, bool>> filter = null);
        void Delete(object entityId);
        void Delete(T entity);
        void Save();
    }
}
