using System;
using System.Collections.Generic;
using System.Text;

namespace WonderwareOnlineSDK.UnitTests.Backend
{
    using WonderwareOnlineSDK.Backend;
    using Xunit;

    public class WonderwareOnlineUploadApiTests
    {
        [Fact]
        public void WonderwareOnlineUploadApi_ConstructorNullHost_Exception()
        {
            ArgumentException argException = (ArgumentException)null;
            try
            {
                var client = new WonderwareOnlineUploadApi(null, "token");
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
        public void WonderwareOnlineUploadApi_ConstructorNullToken_Exception()
        {
            ArgumentException argException = (ArgumentException)null;
            try
            {
                var client = new WonderwareOnlineUploadApi("online.wonderware.com", null);
            }
            catch (ArgumentException argumentException)
            {
                argException = argumentException;
            }

            Assert.NotNull(argException);
            Assert.Equal($"Should not be null or empty{Environment.NewLine}Parameter name: token", argException.Message);
            Assert.Equal("token", argException.ParamName);
        }
    }
}