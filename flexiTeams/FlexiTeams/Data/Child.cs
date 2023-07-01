using System.Xml;

namespace flexiTeams.Data;

public class Child
{
    private int Age { get; }

    Child(XmlReader reader)
    {
        Age = XmlConvert.ToDateTime(reader.ReadInnerXml()).Year;
    }

}