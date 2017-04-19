namespace WonderwareOnlineSDK
{
    using Backend;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Helpers;

    public class WonderwareOnlineClient
    {
        private readonly IWonderwareOnlineUploadApi wonderwareOnlineUploadApi;

        private readonly CollectionBuffer<Tag> tagCollectionBuffer;
        private readonly CollectionBuffer<ProcessValue> processValueCollectionBuffer;

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
            this.processValueCollectionBuffer = new CollectionBuffer<ProcessValue>();
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

            await this.wonderwareOnlineUploadApi.SendTagAsync(tagUploadRequest);
        }

        private async Task PurgeProcessValuesCollectionAsync(IEnumerable<ProcessValue> processValuesBuffer)
        {
            var groups = processValuesBuffer.GroupBy(
               p => Regex.Match(p.Timestamp.ToString("O"), @"\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}.\d{3}"));
            var uploadValueRequest = new DataUploadRequest();

            foreach (var group in groups)
            {
                var timerange = new Dictionary<string, object>();
                timerange.Add("dateTime", group.FirstOrDefault().Timestamp.ToString("O"));
                foreach (var processValue in group)
                {
                    timerange.Add(processValue.TagName, processValue.Value);
                }
                uploadValueRequest.data.Add(timerange);
            }

            await this.wonderwareOnlineUploadApi.SendValueAsync(uploadValueRequest);
        }
    }
}