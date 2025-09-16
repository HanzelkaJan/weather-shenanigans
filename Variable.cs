using System.Xml.Serialization;

namespace WeatherShenanigans;

public class Variable
{
    [XmlElement("sunrise")]
    public string Sunrise { get; set; }
    [XmlElement("sunset")]
    public string Sunset { get; set; }
    [XmlElement("civstart")]
    public string CivStart { get; set; }
    [XmlElement("civend")]
    public string CivEnd { get; set; }
    [XmlElement("nautstart")]
    public string NautStart { get; set; }
    [XmlElement("nautend")]
    public string NautEnd { get; set; }
    [XmlElement("astrostart")]
    public string AstroStart { get; set; }
    [XmlElement("astroend")]
    public string AstroEnd { get; set; }
    [XmlElement("daylen")]
    public string DayLen { get; set; }
    [XmlElement("civlen")]
    public string CivLen { get; set; }
    [XmlElement("nautlen")]
    public string NautLen { get; set; }
    [XmlElement("astrolen")]
    public string AstroLen { get; set; }
    [XmlElement("moonphase")]
    public int MoonPhase { get; set; }
    [XmlElement("isday")]
    public int Day { get; set; }
    [XmlElement("bio")]
    public int Bio { get; set; }
    [XmlElement("pressure_old")]
    public float PressureOld { get; set; }
    [XmlElement("temperature_avg")]
    public float TemperatureAvg { get; set; }
    [XmlElement("agl")]
    public int Agl { get; set; }
    [XmlElement("fog")]
    public int Fog { get; set; }
    [XmlElement("lsp")]
    public int Lsp { get; set; }
    
    
}