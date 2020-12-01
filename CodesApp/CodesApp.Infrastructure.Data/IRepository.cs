using CodesApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CodesApp.Infrastructure.Data
{
    public interface IRepository<T> where T : Entity
    {
        IQueryable<T> QueryAll();
        IQueryable<T> QueryAllIncluding(params Expression<Func<T, object>>[] paths);
        void Add(T item);
        void Delete(T item);
        void Delete(int id);
        T FindById(int id);
        Task<T> FindByIdAsync(int id);
        void DeleteRange(IEnumerable<T> entities);
    }
}
