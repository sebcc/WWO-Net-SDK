namespace WonderwareOnlineSDK.UnitTests.Models
{
    using System;
    using System.Text;
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

        [Theory]
        [InlineData("TagName")]
        [InlineData("Description")]
        [InlineData("Min")]
        [InlineData("Max")]
        [InlineData("DataType")]
        [InlineData("InterpolationType")]
        [InlineData("RolloverValue")]
        [InlineData("EngUnit")]
        [InlineData("IntegralDivisor")]
        [InlineData("TagNAME")]
        [InlineData("DescrIPtion")]
        [InlineData("MIN")]
        [InlineData("mAX")]
        [InlineData("DATAType")]
        [InlineData("InterpolationType")]
        [InlineData("RollOverValue")]
        [InlineData("engUnit")]
        [InlineData("integraldivisor")]
        public void Tag_ExtendedPropertiesReservedPropertie_ExpectException(string prop)
        {
            var tag = new Tag("default_Name");
            NotSupportedException exception = null;

            try
            {
                tag.AddTagExtendedProperty(prop, "SSS");
            }
            catch(NotSupportedException nse)
            {
                exception = nse;
            }

            Assert.NotNull(exception);           
            Assert.Equal("This is a reserved property.", exception.Message);         
        }

        [Fact]
        public void Tag_PropertyNameTooLong_ExpectException()
        {
            var tag = new Tag("default_Name");
            NotSupportedException exception = null;

            try
            {
                tag.AddTagExtendedProperty(GenerateStringWithLength(51), "SSS");
            }
            catch(NotSupportedException nse)
            {
                exception = nse;
            }

            Assert.NotNull(exception);  
            Assert.Equal("Property name too long.", exception.Message);         
        }

        [Fact]
        public void Tag_StringPropertyValueTooLong_ExpectException()
        {
            var tag = new Tag("default_Name");
            NotSupportedException exception = null;

            try
            {
                tag.AddTagExtendedProperty("good_property", GenerateStringWithLength(513));
            }
            catch(NotSupportedException nse)
            {
                exception = nse;
            }

            Assert.NotNull(exception);  
            Assert.Equal("Value is too long.", exception.Message);         
        }

        private string GenerateStringWithLength(int length)
        {
            var builder = new StringBuilder();
            builder.Append('s', length);
            return builder.ToString();
        }
    }
}