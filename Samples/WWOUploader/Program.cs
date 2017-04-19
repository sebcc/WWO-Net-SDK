namespace WWOUploader
{
    using System;
    using System.Diagnostics;
    using WonderwareOnlineSDK;
    using WonderwareOnlineSDK.Models;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var wonderwareOnlineClient = new WonderwareOnlineClient("PROVIDE KEY HERE");
            var stopWatch = Stopwatch.StartNew();

            // Create tag
            var tag = new Tag();
            tag.TagName = "DefaultTag";
            tag.DataType = DataType.Double;
            tag.EngUnit = "ms";

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
                wonderwareOnlineClient.AddProcessValue(tag.TagName, stopWatch.ElapsedMilliseconds);
                Console.WriteLine($"Successfully created process values.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not add process value. Ex.:" + ex);
            }

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