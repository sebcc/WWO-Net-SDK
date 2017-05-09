namespace WonderwareOnlineSDK.UnitTests.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Moqs;
    using Xunit;
    using WonderwareOnlineSDK.Helpers;
    using WonderwareOnlineSDK.Models;

    public class ConverterTests
    {
        [Fact]
        public void Converter_ConvertFromBuffer_ExpectTwoTimerange()
        {
            var processValues = new List<ProcessValue>();
            processValues.Add(new ProcessValue(){Timestamp = new DateTime(2017, 4,19,11,12,13,666,DateTimeKind.Utc), Value = 5, TagName = "Tag1"});
            processValues.Add(new ProcessValue(){Timestamp = new DateTime(2017, 4,19,11,12,13,666,DateTimeKind.Utc), Value = 6, TagName = "Tag2"});
            processValues.Add(new ProcessValue(){Timestamp = new DateTime(2017, 4,19,11,12,13,555,DateTimeKind.Utc), Value = 6, TagName = "Tag1"});
            
            var request = Converter.ConvertFromBuffer(processValues);

            Assert.Equal(2, request.data.Count);
            Assert.Equal(3, request.data.ElementAt(0).Count);
            Assert.Equal(2, request.data.ElementAt(1).Count);
            
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