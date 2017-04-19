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
                var addTagTask = wonderwareOnlineClient.AddTagAsync(tag);
                addTagTask.Wait();
                Console.WriteLine($"Successfully created tag - {tag.TagName}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not create tag. Ex.:" + ex);
            }

            try
            {
                var addValueTask = wonderwareOnlineClient.AddProcessValue(tag.TagName, stopWatch.ElapsedMilliseconds);
                addValueTask.Wait();
                Console.WriteLine($"Successfully created process values.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not add process value. Ex.:" + ex);
            }

            Console.ReadLine();
        }
    }
}