namespace WonderwareOnlineSDK.Backend
{
    using Newtonsoft.Json;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    internal class WonderwareOnlineUploadApi : IWonderwareOnlineUploadApi
    {
        private readonly string key;
        private readonly string uploadApi = "https://online.wonderware.com/apis/upload/datasource";

        public WonderwareOnlineUploadApi(string key)
        {
            this.key = key;
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