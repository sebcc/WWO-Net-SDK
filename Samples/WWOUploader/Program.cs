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
            var wonderwareOnlineClient = new WonderwareOnlineClient("TOKEN HERE");
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