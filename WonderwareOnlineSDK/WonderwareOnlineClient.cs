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

        /// <summary>
        /// Initializes a new instance of the <see cref="WonderwareOnlineClient"/> class.
        /// </summary>
        /// <param name="token">DataSource token</param>
        public WonderwareOnlineClient(string token) :
            this(new WonderwareOnlineUploadApi("online.wonderware.com", token),
                new CollectionBuffer<Tag>(),
                new CollectionBuffer<ProcessValue>(),
                token)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WonderwareOnlineClient"/> class.
        /// </summary>
        /// <param name="hostname">Host name</param>
        /// <param name="token">DataSource token</param>
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

        /// <summary>
        /// Add a process value
        /// </summary>
        /// <param name="tagName">Name of the tag associated</param>
        /// <param name="value">Vallue to add</param>
        public void AddProcessValue(string tagName, string value)
        {
            this.InternalAddProcessValue(tagName, value);
        }

        /// <summary>
        /// Add a process value
        /// </summary>
        /// <param name="tagName">Name of the tag associated</param>
        /// <param name="value">Value to add</param>
        /// <param name="datetime">Datetime of value</param>
        public void AddProcessValue(string tagName, string value, DateTime datetime)
        {
            this.InternalAddProcessValue(tagName, value, datetime);
        }

        /// <summary>
        /// Add a process value
        /// </summary>
        /// <param name="tagName">Name of the tag associated</param>
        /// <param name="value">Value to add</param>
        public void AddProcessValue(string tagName, byte value)
        {
            this.InternalAddProcessValue(tagName, value);
        }

        /// <summary>
        /// Add a process value
        /// </summary>
        /// <param name="tagName">Name of the tag associated</param>
        /// <param name="value">Vallue to add</param>
        /// <param name="datetime">Datetime of value</param>
        public void AddProcessValue(string tagName, byte value, DateTime datetime)
        {
            this.InternalAddProcessValue(tagName, value, datetime);
        }

        /// <summary>
        /// Add a process value
        /// </summary>
        /// <param name="tagName">Name of the tag associated</param>
        /// <param name="value">Vallue to add</param>
        public void AddProcessValue(string tagName, short value)
        {
            this.InternalAddProcessValue(tagName, value);
        }

        /// <summary>
        /// Add a process value
        /// </summary>
        /// <param name="tagName">Name of the tag associated</param>
        /// <param name="value">Vallue to add</param>
        /// <param name="datetime">Datetime of value</param>
        public void AddProcessValue(string tagName, short value, DateTime datetime)
        {
            this.InternalAddProcessValue(tagName, value, datetime);
        }

        /// <summary>
        /// Add a process value
        /// </summary>
        /// <param name="tagName">Name of the tag associated</param>
        /// <param name="value">Vallue to add</param>
        public void AddProcessValue(string tagName, ushort value)
        {
            this.InternalAddProcessValue(tagName, value);
        }

         /// <summary>
        /// Add a process value
        /// </summary>
        /// <param name="tagName">Name of the tag associated</param>
        /// <param name="value">Vallue to add</param>
        /// <param name="datetime">Datetime of value</param>
        public void AddProcessValue(string tagName, ushort value, DateTime datetime)
        {
            this.InternalAddProcessValue(tagName, value, datetime);
        }

        /// <summary>
        /// Add a process value
        /// </summary>
        /// <param name="tagName">Name of the tag associated</param>
        /// <param name="value">Vallue to add</param>
        /// <param name="datetime">Datetime of value</param>
        public void AddProcessValue(string tagName, int value, DateTime datetime)
        {
            this.InternalAddProcessValue(tagName, value, datetime);
        }

        /// <summary>
        /// Add a process value
        /// </summary>
        /// <param name="tagName">Name of the tag associated</param>
        /// <param name="value">Vallue to add</param>
        public void AddProcessValue(string tagName, int value)
        {
            this.InternalAddProcessValue(tagName, value);
        }

        /// <summary>
        /// Add a process value
        /// </summary>
        /// <param name="tagName">Name of the tag associated</param>
        /// <param name="value">Vallue to add</param>
        public void AddProcessValue(string tagName, uint value)
        {
            this.InternalAddProcessValue(tagName, value);
        }

        /// <summary>
        /// Add a process value
        /// </summary>
        /// <param name="tagName">Name of the tag associated</param>
        /// <param name="value">Vallue to add</param>
        /// <param name="datetime">Datetime of value</param>
        public void AddProcessValue(string tagName, uint value, DateTime datetime)
        {
            this.InternalAddProcessValue(tagName, value, datetime);
        }

        /// <summary>
        /// /// Add a process value
        /// </summary>
        /// <param name="tagName">Name of the tag associated</param>
        /// <param name="value">Vallue to add</param>
        public void AddProcessValue(string tagName, float value)
        {
            this.InternalAddProcessValue(tagName, value);
        }

        /// <summary>
        /// /// Add a process value
        /// </summary>
        /// <param name="tagName">Name of the tag associated</param>
        /// <param name="value">Vallue to add</param>
        /// <param name="datetime">Datetime of value</param>
        public void AddProcessValue(string tagName, float value, DateTime datetime)
        {
            this.InternalAddProcessValue(tagName, value, datetime);
        }

        /// <summary>
        /// Add a process value
        /// </summary>
        /// <param name="tagName">Name of the tag associated</param>
        /// <param name="value">Vallue to add</param>
        public void AddProcessValue(string tagName, double value)
        {
            this.InternalAddProcessValue(tagName, value);
        }

        /// <summary>
        /// Add a process value
        /// </summary>
        /// <param name="tagName">Name of the tag associated</param>
        /// <param name="value">Vallue to add</param>
        /// <param name="datetime">Datetime of value</param>
        public void AddProcessValue(string tagName, double value, DateTime datetime)
        {
            this.InternalAddProcessValue(tagName, value, datetime);
        }

        /// <summary>
        /// Add a tag
        /// </summary>
        /// <param name="tag">Tag to add</param>
        public void AddTag(Tag tag)
        {
            if (tag == null)
            {
                throw new ArgumentException("Tag cannot be null", nameof(tag));
            }

            this.tagCollectionBuffer.AddItem(tag);
        }

        /// <summary>
        /// Asynchronously send the buffered data 
        /// </summary>
        /// <returns></returns>
        public async Task PurgeAsync()
        {
            await PurgeTagCollectionAsync(this.tagCollectionBuffer.ExtractBuffer());
            await PurgeProcessValuesCollectionAsync(this.processValueCollectionBuffer.ExtractBuffer());
        }

        /// <summary>
        /// Synchronously send the buffered data
        /// </summary>
        public void Purge()
        {
            try
            {
                PurgeTagCollectionAsync(this.tagCollectionBuffer.ExtractBuffer()).Wait();
                PurgeProcessValuesCollectionAsync(this.processValueCollectionBuffer.ExtractBuffer()).Wait();
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }
        }

        private void InternalAddProcessValue(string tagName, object value, DateTime timestamp = default(DateTime))
        {
            if (string.IsNullOrWhiteSpace(tagName))
            {
                throw new ArgumentException("TagName should not be null or empty", nameof(tagName));
            }

            if (value == null)
            {
                throw new ArgumentException("Value should not be null or empty", nameof(value));
            }

            this.processValueCollectionBuffer.AddItem(
                new ProcessValue() { 
                    TagName = tagName, 
                    Timestamp = timestamp == default(DateTime)?DateTime.UtcNow: timestamp, 
                    Value = value });
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