using System;
using System.Collections;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public class Container<T> : IEnumerable<T> where T: struct, IComparable<T>  
    {
        private T[] _array;

        public Container(params T[] array)
        {
            _array = array;
        }
        public void Sort()
        {
            Array.Sort(_array);
        }

        public void Sort(IComparer comparer)
        {
            Array.Sort(_array, comparer);
        }

        public void Add(T item)
        {
            Array.Resize(ref _array, _array.Length + 1);
            _array[_array.Length - 1] = item;
        }

        public int Count => _array.Length;

        public List<T> ListByCondition(Predicate<T> predicate)
        {
            List<T> temp = new List<T>();
            for (int i = 0; i < _array.Length; i++)
            {
                if (predicate(_array[i]))
                    temp.Add(_array[i]);
            }
            return temp;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_array).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _array.GetEnumerator();
        }

        public T this [int index]
        {
            get => _array[index];
            set => _array[index] = value;
        }
    }
}
