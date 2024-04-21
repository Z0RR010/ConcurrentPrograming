using System.Data.Entity;

namespace Data
{
    public abstract class DataAbstractApi
    {
        public abstract ICollection<T> GetRepository<T>() where T : class;

        public static DataAbstractApi CreateApi()
        {
            return new DataApi();
        }
    }
}
