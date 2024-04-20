namespace Data
{
    public class DataApi<T> : DataAbstractApi<T>
    {
        public override void Add(T item)
        {
            items.Add(item);
        }

        public override void Remove(T item)
        {
            items.Remove(item);
        }

        public override List<T> GetAll()
        {
            return new List<T>(items);
        }
    }
}