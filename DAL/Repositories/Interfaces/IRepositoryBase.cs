using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IRepositoryBase<T> where T:class
    {
        public Task Add(T entity);
        public Task Update(T entity);
        public Task Delete(T entity);
        public Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? expression = null, Func<IQueryable<T>, IQueryable<T>>? includeFunc = null);
        public Task<T> FirstOrDefault(Expression<Func<T, bool>>? expression = null, Func<IQueryable<T>, IQueryable<T>>? includeFunc = null);
        public Task<T> GetById(object id);
    }
}
