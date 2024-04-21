using System;
using System.Collections.Generic;
using System.Data.Entity;
//using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class BallRepository<T> : IRepository<T> where T : class
    {
        private readonly DbContext Context;

        public BallRepository(DbContext context)
        {
            Context = context;
        }

        public void Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                Context.Set<T>().Add(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("Wystąpił błąd zapisu do repozytorium.", ex);
            }

        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                Context.Set<T>().Remove(entity);
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException();
            }
        }

        /*
        public IEnumerable<T> GetAll()
        {
            
        }
        */

        public T Get(int id)
        {
            return Context.Set<T>().Find(id);
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
