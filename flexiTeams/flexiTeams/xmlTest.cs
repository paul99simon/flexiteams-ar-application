namespace flexiTeams;

using System.Xml;
class XmlTest
{
    static void Main()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load("..\\..\\..\\..\\..\\resourcePools\\resource_pool_draft.xml");
        XmlNodeList nodes = doc.DocumentElement.SelectNodes("/resourcePool/resource/firstName[text()='Erika']/../workAgreement");

        int i = 0;
        foreach (XmlNode node in nodes)
        {
            Console.WriteLine(node.InnerText);
        }

    }
}
