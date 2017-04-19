namespace WonderwareOnlineSDK
{
    using Backend;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Helpers;

    public class WonderwareOnlineClient
    {
        private IWonderwareOnlineUploadApi wonderwareOnlineUploadApi;

        private CollectionBuffer<Tag> tagCollectionBuffer;

        public WonderwareOnlineClient(string key) : this(new WonderwareOnlineUploadApi(key), key)
        {
        }

        internal WonderwareOnlineClient(IWonderwareOnlineUploadApi wonderwareOnlineUploadApi, string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("Should not be null or empty", nameof(key));
            }

            this.wonderwareOnlineUploadApi = wonderwareOnlineUploadApi;
            this.tagCollectionBuffer = new CollectionBuffer<Tag>();
        }

        public async Task AddProcessValue(string tagName, object value)
        {
            if (string.IsNullOrWhiteSpace(tagName))
            {
                throw new ArgumentException("TagName should not be null or empty", nameof(tagName));
            }

            if (value == null)
            {
                throw new ArgumentException("Value should not be null or empty", nameof(value));
            }

            var values = new List<Tuple<string, object>>();
            values.Add(new Tuple<string, object>(tagName, value));
            var uploadValueRequest = new DataUploadRequest();
            var timerange = new Dictionary<string, object>();
            timerange.Add("dateTime", DateTime.UtcNow.ToString("O"));

            foreach (var tagValue in values)
            {
                timerange.Add(tagValue.Item1, tagValue.Item2);
            }

            uploadValueRequest.data.Add(timerange);

            await this.wonderwareOnlineUploadApi.SendValueAsync(uploadValueRequest);
        }

        public void AddTag(Tag tag)
        {
            if (tag == null)
            {
                throw new ArgumentException("Tag cannot be null", nameof(tag));
            }

            this.tagCollectionBuffer.AddItem(tag);
        }

        public async Task PurgeAsync()
        {
            await PurgeTagCollectionAsync(this.tagCollectionBuffer.ExtractBuffer());
        }

        private async Task PurgeTagCollectionAsync(IEnumerable<Tag> tagsBuffer)
        {
            var tagUploadRequest = new TagUploadRequest();

            foreach (var tag in tagsBuffer)
            {
                tagUploadRequest.metadata.Add(tag);
            }

            await this.wonderwareOnlineUploadApi.SendTagAsync(tagUploadRequest);
        }
    }
}