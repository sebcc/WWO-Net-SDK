namespace WonderwareOnlineSDK.Backend
{
    using System.Threading.Tasks;

    internal interface IWonderwareOnlineUploadApi
    {
        Task SendTagAsync(TagUploadRequest tagUploadRequest);

        Task SendValueAsync(DataUploadRequest dataUploadRequest);
    }
}