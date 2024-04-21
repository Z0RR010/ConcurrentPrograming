using System;

namespace Data
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        T Get(int id);

        //IEnumerable<T> GetAll();
    }
}
