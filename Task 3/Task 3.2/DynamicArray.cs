using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace CustomTypes
{
    public class DynamicArray<T> : IEnumerable<T>, IEnumerable, ICloneable
    {
        private T[] array;
        private int capacity;

        protected T[] InternalArray => array;

        public int Length { get; set; }
        
        public int Capacity
        {
            get
            {
                return capacity;
            }

            set
            {
                if (value >= 0)
                {
                    capacity = value;

                    Array.Resize(ref array, capacity);

                    if (Length > capacity)
                    {
                        Length = capacity;
                    }
                }
            }
        }

        public DynamicArray()
            : this(8)
        { }

        public DynamicArray(int capacity)
        {
            if (capacity >= 0)
            {
                Capacity = capacity;

                array = new T[Capacity];
            }
            else
            {
                throw new ArgumentOutOfRangeException("Capacity is less than 0");
            }
        }

        public DynamicArray(IEnumerable<T> collection)
            : this(collection.Count())
        {
            if (collection != null)
            {
                array = collection.ToArray();

                Length = array.Length;
            }
            else
            {
                throw new ArgumentNullException("Collection is null");
            }
        }

        public T this[int index]
        {
            get
            {
                if ((index < -Length) || (index >= Length))
                {
                    throw new ArgumentOutOfRangeException();
                }
                else if (index < 0)
                {
                    return array[Length + index];
                }
                else
                {
                    return array[index];
                }
            }
            set
            {
                if ((index < -Length) || (index >= Length))
                {
                    throw new ArgumentOutOfRangeException();
                }
                else if (index < 0)
                {
                    array[Length + index] = value;
                }
                else
                {
                    array[index] = value;
                }
            }
        }

        public void Add(T item)
        {
            if (Capacity == Length)
            {
                Capacity *= 2;
                Array.Resize(ref array, Capacity);
            }

            array[Length] = item;
            Length++;
        }

        public void AddRange(IEnumerable<T> collection)
        {
            if (collection != null)
            {
                bool isResized = false;

                while (Capacity < Length + collection.Count())
                {
                    Capacity *= 2;
                    isResized = true;
                }

                if (isResized)
                {
                    Array.Resize(ref array, Capacity);
                }

                collection.ToArray().CopyTo(array, Length);
                Length += collection.Count();
            }
            else
            {
                throw new ArgumentNullException("Collection is null");
            }
        }

        public bool Remove(T item)
        {
            try
            {
                int index = Array.FindIndex(array, 0, Length, element => element.Equals(item));

                if (index != -1)
                {
                    Array.Copy(array, index + 1, array, index, Length - index - 1);
                    Length--;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool Insert(int index, T item)
        {
            if ((index < 0) || (index > Length))
            {
                throw new ArgumentOutOfRangeException();
            }
            else
            {
                try
                {
                    if (Capacity == Length)
                    {
                        Capacity *= 2;
                        Array.Resize(ref array, Capacity);
                    }

                    Array.Copy(array, index, array, index + 1, Length - index);
                    array[index] = item;
                    Length++;

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public T[] ToArray()
        {
            T[] outArray = new T[Length];

            Array.Copy(array, outArray, Length);

            return outArray;
        }

        public virtual IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Length; i++)
            {
                yield return array[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public object Clone()
        {
            return new DynamicArray<T>(array);
        }
    }
}
