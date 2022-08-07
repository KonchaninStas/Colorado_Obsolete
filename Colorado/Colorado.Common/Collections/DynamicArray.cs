namespace Colorado.Common.Collections
{
    public class DynamicArray<T>
    {
        private int newElementIndex;

        public DynamicArray(int capacity)
        {
            Array = new T[capacity];
        }

        public void Add(T element)
        {
            Array[newElementIndex] = element;
            newElementIndex += 1;
        }

        public void Add(T element, int count)
        {
            for (int i = 0; i < count - 1; i++)
            {
                Add(element);
            }
        }

        public void AddRange(T element, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Add(element);
            }
        }

        public T[] Array { get; }
    }
}
