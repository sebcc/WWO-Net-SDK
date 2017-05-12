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

            Assert.Equal(double.NaN, tag.Min);
            Assert.Equal(double.NaN, tag.Max);
            Assert.Equal("default_Name", tag.TagName);
            Assert.Null(tag.Description);
            Assert.Null(tag.EngUnit);
            Assert.Equal(double.NaN, tag.RolloverValue);
            Assert.Equal(double.NaN, tag.IntegralDivisor);
            Assert.Equal(default(InterpolationType),tag.InterpolationType);
            Assert.Equal(default(DataType),tag.DataType);            
        }
    }
}