namespace WWOUploader
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using WonderwareOnlineSDK;
    using WonderwareOnlineSDK.Models;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var wonderwareOnlineClient = new WonderwareOnlineClient("Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6IjA2MmQ3MWM1LWMyYjEtNGFiZS1hYjc1LWI0MWYzZTViMjk1NyJ9.eyJEYXRhU291cmNlSWQiOiIwNTM0MGY5Yy1iZjY2LTQ2NjMtOTExMi1iN2ZlOTczMjE1ZjkiLCJ0eXBlIjoic2VydmljZSIsInZlcnNpb24iOiIxLjAiLCJ0ZW5hbnRpZCI6IjFkNGExNTZkLTZjYjQtNGExZC1iZDkyLWU3NTM0MjgwMDcxNiIsInNpaWQiOiI2ZTliOGQ5NS0zNDRiLTQ1OTMtODM1OS02NmU2MzEyZmRkODQiLCJqdGkiOiI4Y2JhM2EzNS1lMGY0LTQxZWMtODYxYi1kODJlODY1ZWFhZGUiLCJpc3MiOiJwcm9vZm9maWRlbnRpdHlzZXJ2aWNlIn0.A-lf7GyfzxLo1vij0DizW0924ggP1uB7tY20W2DiZqhzMkEU8SFV9mQeqQ2Dl-UBfi_IlU_RZtsSFuDL5PMByEKHNe1EZgeK2S3GuF7o_Xa8LX6oNkvEgmzn8gIH3QrMdy1CthYEpaZRIhi5LnlsLFZfYVgk-W_36ISpp6MvVqVqI27GZjyPnA5c6aPdkpK7WF2HLJl5h-Cw5qtnihUZF00f_Yim8ux1QhJDg50uLzt6L_9YWCYdIPQmA7ZnPwBJZJ6ZOmqShH9tC2BRIitf9NbcZMpE7nHY6sZkfObVQilfWMfpcTGSG3DLqOMxOZt9pZxHds2YlVz46cx8ASLQjw");
            var stopWatch = Stopwatch.StartNew();

            // Create tag
            var tag = new Tag();
            tag.TagName = "DefaultTag";
            tag.DataType = DataType.Double;
            tag.EngUnit = "ms";
            tag.AddTagExtendedProperty("Prop1", "PropValue");

            try
            {
                wonderwareOnlineClient.AddTag(tag);
                Console.WriteLine($"Successfully created tag - {tag.TagName}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not create tag. Ex.:" + ex);
            }

            try
            {
                for (int i = 0; i < 1000; i++)
                {
                    wonderwareOnlineClient.AddProcessValue(tag.TagName, 1);
                    Thread.Sleep(1);
                }

                Console.WriteLine($"Successfully created process values.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not add process value. Ex.:" + ex);
            }

            // There is an automatic purge. This is only to manually purge the buffer
            try
            {
                var purgeTask = wonderwareOnlineClient.PurgeAsync();
                purgeTask.Wait();
                Console.WriteLine($"Successfully purged.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not purge data. Ex.:" + ex);
            }

            Console.ReadLine();
        }
    }
}