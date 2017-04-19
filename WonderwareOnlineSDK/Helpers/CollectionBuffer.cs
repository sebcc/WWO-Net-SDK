namespace WonderwareOnlineSDK.Helpers
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    internal class CollectionBuffer<T>
    {
        protected ConcurrentQueue<T> Items;

        public CollectionBuffer()
        {
            this.Items = new ConcurrentQueue<T>();
        }

        public void AddItem(T item)
        {
            this.Items.Enqueue(item);
        }

        public IEnumerable<T> ExtractBuffer()
        {
            T item;
            while (this.Items.TryDequeue(out item))
            {
                yield return item;
            }
        }
    }
}