using System;
using System.Collections;
using System.Collections.Generic;

namespace Data
{
    public class Repository<T> : ICollection<T> where T : class
    {
        private readonly List<T> _items = new List<T>();

        public int Count => _items.Count;
        public bool IsReadOnly => false;

        public void Add(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Encja nie może być null.");
            }

            _items.Add(item);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool Contains(T item)
        {
            return _items.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        public bool Remove(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Encja nie może być null.");
            }
            return _items.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
