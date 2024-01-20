using CareerCloud.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.EntityFrameworkDataAccess
{
    public class EFGenericRepository<T> : IDataRepository<T> where T : class
    {
        protected readonly DbContext Context;

        public EFGenericRepository(DbContext context = null)
        {
            Context = context ?? new CareerCloudContext();
        }

        public IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = Context.Set<T>();
            foreach (var navigationProperty in navigationProperties)
            {
                query = query.Include(navigationProperty);
            }

            return query.ToList();
        }

        public IList<T> GetList(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = Context.Set<T>().Where(where);
            foreach (var navigationProperty in navigationProperties)
            {
                query = query.Include(navigationProperty);
            }

            return query.ToList();
        }

        public T GetSingle(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = Context.Set<T>().Where(where);
            foreach (var navigationProperty in navigationProperties)
            {
                query = query.Include(navigationProperty);
            }

            return query.FirstOrDefault();
        }

        public void Add(params T[] items)
        {
            foreach (var item in items)
            {
                Context.Set<T>().Add(item);
            }
        }

        public void Update(params T[] items)
        {
            foreach (var item in items)
            {
                Context.Set<T>().Update(item);
            }
        }

        public void Remove(params T[] items)
        {
            foreach (var item in items)
            {
                Context.Set<T>().Remove(item);
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
        }
    }
}
