namespace WonderwareOnlineSDK.UnitTests
{
    using Backend;
    using Models;
    using Moq;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class WonderwareOnlineClientTests
    {
        [Fact]
        public async Task WonderwareOnlineClient_AddProcessValue_NullTagArgument_ExpectException()
        {
            ArgumentException argException = (ArgumentException)null;
            try
            {
                var client = new WonderwareOnlineClient("Valid key");
                await client.AddProcessValue(null, new object());
            }
            catch (ArgumentException argumentException)
            {
                argException = argumentException;
            }

            Assert.NotNull(argException);
            Assert.Equal("TagName should not be null or empty\r\nParameter name: tagName", argException.Message);
            Assert.Equal("tagName", argException.ParamName);
        }

        [Fact]
        public async Task WonderwareOnlineClient_AddProcessValue_NullValueArgument_ExpectException()
        {
            ArgumentException argException = (ArgumentException)null;
            try
            {
                var client = new WonderwareOnlineClient("Valid key");
                await client.AddProcessValue("tagName", null);
            }
            catch (ArgumentException argumentException)
            {
                argException = argumentException;
            }

            Assert.NotNull(argException);
            Assert.Equal("Value should not be null or empty\r\nParameter name: value", argException.Message);
            Assert.Equal("value", argException.ParamName);
        }

        [Fact]
        public void WonderwareOnlineClient_Constructor_Exception()
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
            Assert.Equal("Should not be null or empty\r\nParameter name: key", argException.Message);
            Assert.Equal("key", argException.ParamName);
        }

        [Fact]
        public async Task WonderwareOnlineClient_SendTag_BackendCalled()
        {
            var apiMock = new Mock<IWonderwareOnlineUploadApi>();
            var tag = new Tag();
            tag.TagName = Guid.NewGuid().ToString();
            var client = new WonderwareOnlineClient(apiMock.Object, "Valid key");
            await client.AddTagAsync(tag);

            apiMock.Verify(a => a.SendTagAsync(It.Is<TagUploadRequest>(t => t.metadata.ElementAt(0).TagName.Equals(tag.TagName))), Times.Once);
        }

        [Fact]
        public async Task WonderwareOnlineClient_SendTagNullArgument_ExpectException()
        {
            ArgumentException argException = (ArgumentException)null;
            try
            {
                var client = new WonderwareOnlineClient("Valid key");
                await client.AddTagAsync(null);
            }
            catch (ArgumentException argumentException)
            {
                argException = argumentException;
            }

            Assert.NotNull(argException);
            Assert.Equal("Tag cannot be null\r\nParameter name: tag", argException.Message);
            Assert.Equal("tag", argException.ParamName);
        }
    }
}