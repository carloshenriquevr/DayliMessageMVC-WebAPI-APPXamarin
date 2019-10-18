using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DailyMessageMVC.Data
{
    public interface IUnitOfWork<T> where T : class
    {
        Task<int> SaveAsync(T model);
        Task<int> UpadateAsync(T model);
        void Delete(T model);
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(object id);
        IEnumerable<T> Where(Expression<Func<T, bool>> expression);
        IEnumerable<T> OrderBy(Expression<System.Func<T, bool>> expression);
        IQueryable<T> QueryAll();
        int ExecuteCommand(string spQuery, object[] parameters);
    }
}
