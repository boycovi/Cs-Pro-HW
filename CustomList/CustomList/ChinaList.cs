using System.Collections;

namespace CustomList
{
    internal class ChinaList<T> : IEnumerable<T>
    {
        private T[] _data;

        private int _capacity;
        private int _count;

        public event EventHandler OnExpand;

        public int Capacity => _capacity;
        public int Count => _count;


        public ChinaList()
        {
            _data = new T[0];
        }

        public void Add(T item)
        {
            if (_data.Length == 0) _data = new T[] { item };
            else
            {
                Array.Resize(ref _data, _data.Length + 1);
                _data[_data.Length - 1] = item;
                OnExpand?.Invoke(this, EventArgs.Empty);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_data).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }
    }
}