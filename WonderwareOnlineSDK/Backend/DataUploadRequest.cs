namespace WonderwareOnlineSDK.Backend
{
    using System.Collections.Generic;

    internal class DataUploadRequest
    {
        public DataUploadRequest()
        {
            this.data = new List<Dictionary<string, object>>();
        }

        public List<Dictionary<string, object>> data { get; set; }
    }
}