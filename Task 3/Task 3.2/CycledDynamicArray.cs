using System.Collections.Generic;

namespace CustomTypes
{
    public sealed class CycledDynamicArray<T> : DynamicArray<T>
    {
        public CycledDynamicArray()
            : base()
        { }

        public CycledDynamicArray(int capacity)
            : base(capacity)
        { }

        public CycledDynamicArray(IEnumerable<T> collection)
            : base(collection)
        { }

        public override IEnumerator<T> GetEnumerator()
        {
            int i = 0;

            while(true)
            {
                if (i == Length)
                {
                    i = 0;
                }

                yield return InternalArray[i++];
            }
        }
    }
}
