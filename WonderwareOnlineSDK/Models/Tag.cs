using System.Collections.Generic;

namespace WonderwareOnlineSDK.Models
{
    public class Tag
    {
        public Tag() : this(string.Empty)
        {
        }

        public Tag(string tagName)
        {
            this.TagName = tagName;
            this.DataType = DataType.Double;
            this.Min = 0;
            this.Max = 100;
        }

        public string TagName { get; set; }

        public string EngUnit { get; set; }

        public DataType DataType { get; set; }

        public double Min { get; set; }

        public double Max { get; set; }

        public string Description { get; set; }

        public InterpolationType InterpolationType { get; set; }

        public double IntegralDivisor { get; set; }

        public double RolloverValue { get; set; }

        private Dictionary<string, TagExtendedPropertyValue> tagExtendedProperties = new Dictionary<string, TagExtendedPropertyValue>();

        public void AddTagExtendedProperty (string newProperty, string dataType, object value)
        {
            this.tagExtendedProperties.Add(newProperty, new TagExtendedPropertyValue(){ DataType = dataType, Value = value });
        }
    }
}