using System;
using System.Linq;
using System.Reflection;
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
            this.Min = double.NaN;
            this.Max = double.NaN;
            this.IntegralDivisor = double.NaN;
            this.RolloverValue = double.NaN;
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

        public void AddTagExtendedProperty(string newProperty, string value)
        {
            if (value.Length > 512)
            {
                throw new NotSupportedException("Value is too long.");                
            }

            this.AddTagExtendedProperty(newProperty, "String", value);
        }

        public void AddTagExtendedProperty(string newProperty, short value)
        {
            this.AddTagExtendedProperty(newProperty, "Int16", value);
        }

        public void AddTagExtendedProperty(string newProperty, int value)
        {
            this.AddTagExtendedProperty(newProperty, "Int32", value);
        }

        public void AddTagExtendedProperty(string newProperty, long value)
        {
            this.AddTagExtendedProperty(newProperty, "Int64", value);
        }
        public void AddTagExtendedProperty(string newProperty, Guid value)
        {
            this.AddTagExtendedProperty(newProperty, "Guid", value);
        }

        public void AddTagExtendedProperty(string newProperty, DateTimeOffset value)
        {
            this.AddTagExtendedProperty(newProperty, "DateTimeOffset", value);
        }

        public void AddTagExtendedProperty(string newProperty, double value)
        {
            this.AddTagExtendedProperty(newProperty, "Double", value);
        }

        public void AddTagExtendedProperty(string newProperty, bool value)
        {
            this.AddTagExtendedProperty(newProperty, "Boolean", value);
        }

        private void AddTagExtendedProperty (string newProperty, string dataType, object value)
        {
            if (newProperty.Equals("TagName",StringComparison.OrdinalIgnoreCase)||
                newProperty.Equals("Description",StringComparison.OrdinalIgnoreCase)||
                newProperty.Equals("EngUnit",StringComparison.OrdinalIgnoreCase)||
                newProperty.Equals("DataType",StringComparison.OrdinalIgnoreCase)||
                newProperty.Equals("Min",StringComparison.OrdinalIgnoreCase)||
                newProperty.Equals("Max",StringComparison.OrdinalIgnoreCase)||
                newProperty.Equals("InterpolationType",StringComparison.OrdinalIgnoreCase)||
                newProperty.Equals("RolloverValue",StringComparison.OrdinalIgnoreCase)||
                newProperty.Equals("IntegralDivisor",StringComparison.OrdinalIgnoreCase))
            {
                throw new NotSupportedException("This is a reserved property.");
            }

            if (newProperty.Length > 50)
            {
                throw new NotSupportedException("Property name too long.");
            }

            this.tagExtendedProperties.Add(newProperty, new TagExtendedPropertyValue(){ DataType = dataType, Value = value });
        }

        public Dictionary<string, object> ToDictionary()
        {
            var valueToReturn = new Dictionary<string,object>();
            valueToReturn.Add("TagName", this.TagName);
            valueToReturn.Add("Min", this.Min);
            valueToReturn.Add("Max", this.Max);
            valueToReturn.Add("Description", this.Description);
            valueToReturn.Add("EngUnit", this.EngUnit);
            valueToReturn.Add("DataType", this.DataType.ToString());
            valueToReturn.Add("InterpolationType", this.InterpolationType.ToString());
            valueToReturn.Add("IntegralDivisor", this.IntegralDivisor);
            valueToReturn.Add("RolloverValue", this.RolloverValue);
            
            foreach(var tagExtendedProperty in this.tagExtendedProperties)
            {
                valueToReturn.Add(tagExtendedProperty.Key, tagExtendedProperty.Value);
            }

            return valueToReturn;
        }
    }
}