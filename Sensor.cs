using System.Xml.Serialization;

namespace WeatherShenanigans;

public class Sensor
{
    [XmlElement("type")]
    public string Type { get; set; }
    [XmlElement("id")]
    public string Id { get; set; }
    [XmlElement("name")]
    public string Name { get; set; }
    [XmlElement("place")]
    public string Place { get; set; }
    [XmlElement("value")]
    public string Value { get; set; }
}