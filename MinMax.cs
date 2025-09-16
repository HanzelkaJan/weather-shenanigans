using System.Xml.Serialization;

namespace WeatherShenanigans;

public class MinMax
{
    [XmlAttribute("id")]
    public string Id { get; set; }
    [XmlAttribute("min")]
    public float Min { get; set; }
    [XmlAttribute("max")]
    public float Max { get; set; }
}