using System.Data.Entity;

namespace Data
{
    public class DataApi : DataAbstractApi
    {
        protected readonly DbContext Context;
        public DataApi(DbContext context)
        {
        }

        public override IRepository<T> GetRepository<T>()
        {
            return new BallRepository<T>(Context);
        }
    }
}