namespace WonderwareOnlineSDK.Backend
{
    using System;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;    
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    internal class WonderwareOnlineUploadApi : IWonderwareOnlineUploadApi
    {
        private readonly string token;
        private readonly string hostname;
        private readonly string uploadApi;

        public WonderwareOnlineUploadApi(string hostname, string token)
        {
            if (string.IsNullOrWhiteSpace(hostname))
            {
                throw new ArgumentException("Should not be null or empty", nameof(hostname));
            }

            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentException("Should not be null or empty", nameof(token));
            }

            this.token = token;
            this.hostname = hostname;
            this.uploadApi = $"https://{hostname}/apis/upload/datasource";
        }

        public async Task SendTagAsync(TagUploadRequest tagUploadRequest)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"{this.token}");

                var result = await client.PostAsync(uploadApi,
                    new StringContent(JsonConvert.SerializeObject(tagUploadRequest, new StringEnumConverter()), Encoding.UTF8, "application/json"));

                result.EnsureSuccessStatusCode();
            }
        }

        public async Task SendValueAsync(DataUploadRequest dataUploadRequest)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"{this.token}");

                var result = await client.PostAsync(uploadApi,
                    new StringContent(JsonConvert.SerializeObject(dataUploadRequest), Encoding.UTF8, "application/json"));

                result.EnsureSuccessStatusCode();
            }
        }
    }
}