using System.Xml.Serialization;

namespace WeatherShenanigans;

[XmlRoot("wario")]
public class Wario
{
    //<wario degree="C" pressure="hPa" serial_number="0:50:c2:af:57:49" model="ME13" firmware="ME220014" runtime="1236" freemem="32464" date="2025-5-20" time="14:26:10" language="-1" pressure_type="1" r="0" bip="0">
    [XmlAttribute("degree")]
    public string Degree { get; set; }
    [XmlAttribute("pressure")]
    public string Pressure { get; set; }
    [XmlAttribute("serial_number")]
    public string SerialNumber { get; set; }
    [XmlAttribute("model")]
    public string Model { get; set; }
    [XmlAttribute("firmware")]
    public string Firmware { get; set; }
    [XmlAttribute("runtime")]
    public int Runtime { get; set; }
    [XmlAttribute("freemem")]
    public int FreeMem { get; set; }
    [XmlAttribute("date")]
    public string Date { get; set; }
    [XmlAttribute("time")]
    public DateTime Time { get; set; }
    [XmlAttribute("language")]
    public int Language { get; set; }
    [XmlAttribute("pressure_type")]
    public int PressureType { get; set; }
    [XmlAttribute("r")]
    public int R { get; set; }
    [XmlAttribute("bip")]
    public int Bip { get; set; }
    [XmlArray("input")]
    [XmlArrayItem("sensor")]
    public Sensor[] Input { get; set; }
    [XmlArray("output")]
    [XmlArrayItem("sensor")]
    public Sensor[] Output { get; set; }
    [XmlElement("variable")]
    public Variable Variable { get; set; }
    [XmlArray("minmax")]
    [XmlArrayItem("s")]
    public MinMax[] MinMax { get; set; }
}