namespace WonderwareOnlineSDK.UnitTests.Moqs
{
    using Helpers;
    using WonderwareOnlineSDK.Helpers;

    internal class CollectionBufferMoq<T> : CollectionBuffer<T>
    {
        public CollectionBufferMoq(T[] defaultItems)
        {
            foreach (var defaultItem in defaultItems)
            {
                this.Items.Enqueue(defaultItem);
            }
        }

        public int ItemCount => this.Items.Count;
    }
}