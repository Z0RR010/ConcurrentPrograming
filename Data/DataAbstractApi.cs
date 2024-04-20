namespace Data
{
    public abstract class DataAbstractApi<T>
    {
        protected List<T> items = new List<T>();

        public abstract void Add(T item);
        public abstract void Remove(T item);
        public abstract List<T> GetAll();
    }
}
