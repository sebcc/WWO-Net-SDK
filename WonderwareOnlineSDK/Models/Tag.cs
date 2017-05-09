namespace WonderwareOnlineSDK.Models
{
    public class Tag
    {
        public string TagName { get; set; }

        public string EngUnit { get; set; }

        public DataType DataType { get; set; }

        public double Min { get; set; }

        public double Max { get; set; }

        public string Description { get; set; }

        public InterpolationType InterpolationType { get; set; }

        public double IntegralDivisor { get; set; }

        public double RolloverValue { get; set; }

        public static Tag CreateDefault(string tagName)
        {
            var tag = new Tag();
            tag.TagName = tagName;
            tag.DataType = DataType.Float;
            tag.Min = 0;
            tag.Max = 100;
            
            return tag;
        }
    }
}