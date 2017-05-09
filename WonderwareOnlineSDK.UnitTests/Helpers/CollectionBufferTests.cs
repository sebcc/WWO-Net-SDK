namespace WonderwareOnlineSDK.UnitTests.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Moqs;
    using Xunit;
    using WonderwareOnlineSDK.Models;

    public class CollectionBufferTests
    {
        [Fact]
        public void CollectionBuffer_ExtractBuffer_NoData()
        {
            var collectionBuffer = new CollectionBufferMoq<Tag>(BuildTagCollection(150));

            var extractedBufferCount = collectionBuffer.ExtractBuffer().Count();

            Assert.Equal(150, extractedBufferCount);
        }

        private Tag[] BuildTagCollection(int count)
        {
            var tags = new List<Tag>();
            for (int i = 0; i < count; i++)
            {
                tags.Add(new Tag() { TagName = Guid.NewGuid().ToString() });
            }

            return tags.ToArray();
        }
    }
}