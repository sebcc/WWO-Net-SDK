namespace WonderwareOnlineSDK.Helpers
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using Backend;
    using Models;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System;

    internal static class Converter
    {
        public static DataUploadRequest ConvertFromBuffer(IEnumerable<ProcessValue> buffer)
        {
            var groups = buffer.GroupBy(
               p => $"{p.Timestamp.Year}{p.Timestamp.Month}{p.Timestamp.Day}{p.Timestamp.Hour}{p.Timestamp.Minute}{p.Timestamp.Second}{p.Timestamp.Millisecond.ToString("000")}");
            var uploadValueRequest = new DataUploadRequest();

            foreach (var group in groups)
            {
                var timerange = new Dictionary<string, object>();
                timerange.Add("dateTime", group.FirstOrDefault().Timestamp.ToString("O"));
                foreach (var processValue in group)
                {
                    try
                    {
                        timerange.Add(processValue.TagName, processValue.Value);
                    }
                    catch(ArgumentException)
                    {
                    }
                }

                uploadValueRequest.data.Add(timerange);
            }

            return uploadValueRequest;
        }
    }
}