namespace WonderwareOnlineSDK.Backend
{
    using System.Collections.Generic;
    using Models;

    internal class TagUploadRequest
    {
        public TagUploadRequest()
        {
            this.metadata = new List<Tag>();
        }

        public List<Tag> metadata { get; set; }
    }
}