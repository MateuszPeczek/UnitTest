using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestProject.Models;

namespace TestProject.Repository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        IEnumerable<T> SelectAll();
        IEnumerable<T> SelectById(Expression<Func<T, bool>> predicate);
        T Create(T entity);
        T Delete(T entity);
        void Update(T entity);
        void Save();
    }
}
