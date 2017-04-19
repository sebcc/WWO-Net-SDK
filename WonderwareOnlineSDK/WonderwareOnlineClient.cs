namespace WonderwareOnlineSDK
{
    using Backend;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class WonderwareOnlineClient
    {
        private IWonderwareOnlineUploadApi wonderwareOnlineUploadApi;

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

        public async Task AddTagAsync(Tag tag)
        {
            if (tag == null)
            {
                throw new ArgumentException("Tag cannot be null", nameof(tag));
            }

            var tagUploadRequest = new TagUploadRequest();
            tagUploadRequest.metadata.Add(tag);
            await this.wonderwareOnlineUploadApi.SendTagAsync(tagUploadRequest);
        }
    }
}