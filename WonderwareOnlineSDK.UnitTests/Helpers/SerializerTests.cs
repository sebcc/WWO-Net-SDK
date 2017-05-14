namespace WonderwareOnlineSDK.UnitTests.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Moqs;
    using Xunit;
    using WonderwareOnlineSDK.Helpers;
    using WonderwareOnlineSDK.Models;
    using WonderwareOnlineSDK.Backend;

    public class SerializerTests
    {
        [Fact]
        public void Serializer_TagRequestSerializer_ExtendedProperty()
        {
            var tag = new Tag("tag1");
            tag.AddTagExtendedProperty("prop1","object");
            var tagRequest = new TagUploadRequest();
            tagRequest.metadata.Add(tag);


            var result = Serializer.Serialize(tagRequest);
            Assert.Equal("{\"metadata\":[{\"TagName\":\"tag1\",\"DataType\":\"Double\",\"InterpolationType\":\"Linear\",\"prop1\":{\"DataType\":\"String\",\"Value\":\"object\"}}]}",result);
            
        }

        [Fact]
        public void Converter_DuplicateProcessValue_ExpectSingle()
        {
            var processValues = new List<ProcessValue>();
            processValues.Add(new ProcessValue(){Timestamp = new DateTime(2017, 4,19,11,12,13,666,DateTimeKind.Utc), Value = 5, TagName = "Tag1"});
            processValues.Add(new ProcessValue(){Timestamp = new DateTime(2017, 4,19,11,12,13,666,DateTimeKind.Utc), Value = 6, TagName = "Tag1"});
            
            var request = Converter.ConvertFromBuffer(processValues);

            Assert.Equal(1, request.data.Count);
            Assert.Equal(2, request.data.ElementAt(0).Count);
            
        }
    }
}