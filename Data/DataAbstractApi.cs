using System.Data.Entity;

namespace Data
{
    public abstract class DataAbstractApi
    {
        //protected readonly DbContext Context;
        /*
        protected DataAbstractApi(DbContext context)
        {
            Context = context;
        }
        */
        public abstract IRepository<T> GetRepository<T>() where T : class;
    }
}
