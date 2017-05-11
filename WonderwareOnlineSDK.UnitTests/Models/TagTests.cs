namespace WonderwareOnlineSDK.UnitTests.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Moqs;
    using Xunit;
    using WonderwareOnlineSDK.Helpers;
    using WonderwareOnlineSDK.Models;

    public class TagTests
    {
        [Fact]
        public void Tag_DefaultValue_Success()
        {
            var tag = new Tag("default_Name");

            Assert.Equal(0, tag.Min);
            Assert.Equal(100, tag.Max);
            Assert.Equal("default_Name", tag.TagName);
            Assert.Null(tag.Description);
            Assert.Null(tag.EngUnit);
            Assert.Equal(default(double), tag.RolloverValue);
            Assert.Equal(default(double), tag.IntegralDivisor);
            Assert.Equal(default(InterpolationType),tag.InterpolationType);
            Assert.Equal(default(DataType),tag.DataType);            
        }
    }
}