using CodesApp.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CodesApp.Infrastructure.Data.Ef
{
    public class EntityFrameworkRepository<T> : IRepository<T> where T : Entity
    {
        protected CodesContext DbContext { get; }
        protected DbSet<T> DbSet { get; }

        public EntityFrameworkRepository(CodesContext context)
        {
            DbContext = context;
            DbSet = context.Set<T>();
        }

        public IQueryable<T> QueryAll()
        {
            return DbSet;
        }

        public T FindById(int id)
        {
            return DbSet.Find(id);
        }

        public async Task<T> FindByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public void Add(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            DbSet.Add(item);
        }
        public void Delete(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            DbSet.Remove(item);
        }

        public void Delete(int id)
        {
            T item = FindById(id);
            if (item != null)
            {
                Delete(item);
            }
        }
        public void DeleteRange(IEnumerable<T> entities)
        {
            DbSet.RemoveRange(entities);
        }

        public IQueryable<T> QueryAllIncluding(params Expression<Func<T, object>>[] paths)
        {
            if (paths == null)
            {
                throw new ArgumentNullException(nameof(paths));
            }

            return paths.Aggregate<Expression<Func<T, object>>, IQueryable<T>>(DbSet,
                (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
