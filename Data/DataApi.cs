using System.Collections.ObjectModel;
using System.Data.Entity;

namespace Data
{
    public class DataApi : DataAbstractApi
    {
        protected readonly DbContext Context;

        public override ICollection<T> GetRepository<T>()
        {
            return new Repository<T>();
        }
    }
}