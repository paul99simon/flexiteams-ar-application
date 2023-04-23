namespace flexiTeams;

using System.Xml;
class XmlTest
{
    static void Main()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load("..\\..\\..\\..\\..\\resourcePools\\resource_pool_draft.xml");
        XmlNode node = doc.DocumentElement.SelectSingleNode("/resourcePool/resource/name/firstName[@v='Erika']/../..");
        Console.WriteLine(node.Attributes.GetNamedItem("age").Value);
    }
}
