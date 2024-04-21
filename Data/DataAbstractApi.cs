using System.Data.Entity;

namespace Data
{
    public abstract class DataAbstractApi
    {
        public abstract IRepository<T> GetRepository<T>() where T : class;
    }
}
