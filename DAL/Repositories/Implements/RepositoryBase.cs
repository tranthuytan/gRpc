using DAL.Repositories.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL.Repositories.Implements
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly BookStoreLab2Context _context;
        public DbSet<T> _dbSet;

        public RepositoryBase(BookStoreLab2Context context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task Add(T entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? expression = null, Func<IQueryable<T>, IQueryable<T>>? includeFunc = null)
        {
            IQueryable<T> query = _dbSet;
            if (expression!=null)
            {
                query = query.Where(expression);
            }
            if (includeFunc!=null)
            {
                query = includeFunc(query);
            }
            return await query.ToListAsync();
        }
        public async Task<T> FirstOrDefault(Expression<Func<T, bool>>? expression = null, Func<IQueryable<T>, IQueryable<T>>? includeFunc = null)
        {
            IQueryable<T> query = _dbSet;
            if (expression != null)
            {
                query = query.Where(expression);
            }
            if (includeFunc != null)
            {
                query = includeFunc(query);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<T> GetById(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task Update(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
