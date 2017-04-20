namespace WonderwareOnlineSDK.Backend
{
    using System;
    using Newtonsoft.Json;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    internal class WonderwareOnlineUploadApi : IWonderwareOnlineUploadApi
    {
        private readonly string key;
        private readonly string hostname;
        private readonly string uploadApi;

        public WonderwareOnlineUploadApi(string hostname, string key)
        {
            if (string.IsNullOrWhiteSpace(hostname) || !hostname.StartsWith("online.wonderware"))
            {
                throw new ArgumentException("Should not be null or empty or start with 'online.wonderware'", nameof(hostname));
            }

            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("Should not be null or empty", nameof(key));
            }

            this.key = key;
            this.hostname = hostname;
            this.uploadApi = $"https://{hostname}/apis/upload/datasource";
        }

        public async Task SendTagAsync(TagUploadRequest tagUploadRequest)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"{this.key}");

                var result = await client.PostAsync(uploadApi,
                    new StringContent(JsonConvert.SerializeObject(tagUploadRequest), Encoding.UTF8, "application/json"));

                result.EnsureSuccessStatusCode();
            }
        }

        public async Task SendValueAsync(DataUploadRequest dataUploadRequest)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"{this.key}");

                var result = await client.PostAsync(uploadApi,
                    new StringContent(JsonConvert.SerializeObject(dataUploadRequest), Encoding.UTF8, "application/json"));

                result.EnsureSuccessStatusCode();
            }
        }
    }
}