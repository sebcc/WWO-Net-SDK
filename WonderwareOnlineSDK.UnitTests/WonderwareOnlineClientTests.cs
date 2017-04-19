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
            Assert.Equal($"Should not be null or empty{Environment.NewLine}Parameter name: key", argException.Message);
            Assert.Equal("key", argException.ParamName);
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
    }
}