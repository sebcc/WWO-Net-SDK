namespace WonderwareOnlineSDK
{
    using Backend;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using BackgroundTasks;
    using Helpers;

    public class WonderwareOnlineClient
    {
        private readonly IWonderwareOnlineUploadApi wonderwareOnlineUploadApi;

        private readonly CollectionBuffer<Tag> tagCollectionBuffer;
        private readonly CollectionBuffer<ProcessValue> processValueCollectionBuffer;
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private PurgeTask purgeTask;

        public WonderwareOnlineClient(string token) :
            this(new WonderwareOnlineUploadApi("online.wonderware.com", token),
                new CollectionBuffer<Tag>(),
                new CollectionBuffer<ProcessValue>(),
                token)
        {
        }

        public WonderwareOnlineClient(string hostname, string token) :
           this(new WonderwareOnlineUploadApi(hostname, token),
               new CollectionBuffer<Tag>(),
               new CollectionBuffer<ProcessValue>(),
               token)
        {
        }

        internal WonderwareOnlineClient(
            IWonderwareOnlineUploadApi wonderwareOnlineUploadApi,
            CollectionBuffer<Tag> tagBuffer,
            CollectionBuffer<ProcessValue> processValueBuffer,
            string key)
        {
            this.wonderwareOnlineUploadApi = wonderwareOnlineUploadApi;
            this.tagCollectionBuffer = tagBuffer;
            this.processValueCollectionBuffer = processValueBuffer;
            this.purgeTask = new PurgeTask(cancellationTokenSource.Token, 5000, PurgeAsync);
        }

        public void AddProcessValue(string tagName, object value)
        {
            if (string.IsNullOrWhiteSpace(tagName))
            {
                throw new ArgumentException("TagName should not be null or empty", nameof(tagName));
            }

            if (value == null)
            {
                throw new ArgumentException("Value should not be null or empty", nameof(value));
            }

            this.processValueCollectionBuffer.AddItem(new ProcessValue() { TagName = tagName, Timestamp = DateTime.UtcNow, Value = value });
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
            await PurgeProcessValuesCollectionAsync(this.processValueCollectionBuffer.ExtractBuffer());
        }

        private async Task PurgeTagCollectionAsync(IEnumerable<Tag> tagsBuffer)
        {
            var tagUploadRequest = new TagUploadRequest();

            foreach (var tag in tagsBuffer)
            {
                tagUploadRequest.metadata.Add(tag);
            }

            if (tagUploadRequest.metadata.Any())
            {
                await this.wonderwareOnlineUploadApi.SendTagAsync(tagUploadRequest);
            }
        }

        private async Task PurgeProcessValuesCollectionAsync(IEnumerable<ProcessValue> processValuesBuffer)
        {
            var request = Converter.ConvertFromBuffer(processValuesBuffer);

            if (request.data.Any())
            {
                await this.wonderwareOnlineUploadApi.SendValueAsync(request);
            }
        }
    }
}