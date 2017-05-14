namespace WonderwareOnlineSDK.Helpers
{
    using System;
    using System.Diagnostics;
    using System.Dynamic;  
    using System.Collections.Generic;  
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using WonderwareOnlineSDK.Backend;
    internal static class Serializer
    {
        public static string Serialize(TagUploadRequest uploadRequest)
        {
            dynamic temp = new ExpandoObject();
            temp.metadata = new List<Dictionary<string,object>>();
            
            foreach(var tag in uploadRequest.metadata)
            {
                var tagTemp = new Dictionary<string, object>();
                foreach(var item in tag.ToDictionary())
                {
                    if(item.Value != null)
                    {
                        if (item.Value.GetType() == typeof(double) &&  item.Value.Equals(double.NaN))
                        {
                            continue;
                        }
                        tagTemp.Add(item.Key, item.Value);
                    }
                }
                temp.metadata.Add(tagTemp);
            }
            return JsonConvert.SerializeObject(temp, new StringEnumConverter(), new KeyValuePairConverter());
        }
    }
}