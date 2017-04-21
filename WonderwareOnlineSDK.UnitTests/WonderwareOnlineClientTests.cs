namespace WonderwareOnlineSDK.UnitTests
{
    using Backend;
    using Models;
    using Moq;
    using Moqs;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using WonderwareOnlineSDK.Backend;
    using Xunit;

    public class WonderwareOnlineClientTests
    {
        [Fact]
        public void WonderwareOnlineClient_AddProcessValue_NullTagArgument_ExpectException()
        {
            ArgumentException argException = (ArgumentException)null;
            try
            {
                var client = new WonderwareOnlineClient("Valid key");
                client.AddProcessValue(null, new object());
            }
            catch (ArgumentException argumentException)
            {
                argException = argumentException;
            }

            Assert.NotNull(argException);
            Assert.Equal($"TagName should not be null or empty{Environment.NewLine}Parameter name: tagName", argException.Message);
            Assert.Equal("tagName", argException.ParamName);
        }

        [Fact]
        public void WonderwareOnlineClient_AddProcessValue_NullValueArgument_ExpectException()
        {
            ArgumentException argException = (ArgumentException)null;
            try
            {
                var client = new WonderwareOnlineClient("Valid key");
                client.AddProcessValue("tagName", null);
            }
            catch (ArgumentException argumentException)
            {
                argException = argumentException;
            }

            Assert.NotNull(argException);
            Assert.Equal($"Value should not be null or empty{Environment.NewLine}Parameter name: value", argException.Message);
            Assert.Equal("value", argException.ParamName);
        }

        [Fact]
        public void WonderwareOnlineClient_Constructor_NullKey()
        {
            ArgumentException argException = (ArgumentException)null;
            try
            {
                var client = new WonderwareOnlineClient(null);
            }
            catch (ArgumentException argumentException)
            {
                argException = argumentException;
            }

            Assert.NotNull(argException);
            Assert.Equal($"Should not be null or empty{Environment.NewLine}Parameter name: key", argException.Message);
            Assert.Equal("key", argException.ParamName);
        }

        [Fact]
        public void WonderwareOnlineClient_ConstructorNullHost_ExpectException()
        {
            ArgumentException argException = (ArgumentException)null;
            try
            {
                var client = new WonderwareOnlineClient(null, "validKey");
            }
            catch (ArgumentException argumentException)
            {
                argException = argumentException;
            }

            Assert.NotNull(argException);
            Assert.Equal($"Should not be null or empty{Environment.NewLine}Parameter name: hostname", argException.Message);
            Assert.Equal("hostname", argException.ParamName);
        }

        [Fact]
        public void WonderwareOnlineClient_SendTagNullArgument_ExpectException()
        {
            ArgumentException argException = (ArgumentException)null;
            try
            {
                var client = new WonderwareOnlineClient("Valid key");
                client.AddTag(null);
            }
            catch (ArgumentException argumentException)
            {
                argException = argumentException;
            }

            Assert.NotNull(argException);
            Assert.Equal($"Tag cannot be null{Environment.NewLine}Parameter name: tag", argException.Message);
            Assert.Equal("tag", argException.ParamName);
        }

        [Fact]
        public async Task WonderwareOnlineClient_Purge_ExpectTagAndProcessValue()
        {
            // SETUP
            var tags = new List<Tag>();
            tags.Add(new Tag() { TagName = "Tag1" });

            var processValues = new List<ProcessValue>();
            processValues.Add(new ProcessValue() { Timestamp = new DateTime(2017, 4, 19, 11, 12, 13, 666, DateTimeKind.Utc), Value = 5, TagName = "Tag1" });
            processValues.Add(new ProcessValue() { Timestamp = new DateTime(2017, 4, 19, 11, 12, 13, 666, DateTimeKind.Utc), Value = 6, TagName = "Tag2" });
            processValues.Add(new ProcessValue() { Timestamp = new DateTime(2017, 4, 19, 11, 12, 13, 555, DateTimeKind.Utc), Value = 6, TagName = "Tag1" });

            var tagBuffer = new CollectionBufferMoq<Tag>(tags.ToArray());
            var processValueBuffer = new CollectionBufferMoq<ProcessValue>(processValues.ToArray());
            var apiMock = new Mock<IWonderwareOnlineUploadApi>();

            // ACTION
            var client = new WonderwareOnlineClient(apiMock.Object, tagBuffer, processValueBuffer, "Valid Key");
            await client.PurgeAsync();

            // ASSERT
            Assert.Equal(0, tagBuffer.ItemCount);
            Assert.Equal(0, processValueBuffer.ItemCount);
            apiMock.Verify(a => a.SendTagAsync(It.Is<TagUploadRequest>(t => t.metadata.Count == 1)), Times.Once);
            apiMock.Verify(a => a.SendValueAsync(It.Is<DataUploadRequest>(d => d.data.Count == 2)), Times.Once);
        }
    }
}