using FlexiTeams.ConstructionClasses.Builder;
using FlexiTeams.ConstructionClasses.Diretor;
using FlexiTeams.DataClasses.Resource;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace FlexiTeams.ConstructionClasses.Director
{
    public class BasicResourcePoolDirector
    {
        public static void ConsructFromXML(ResourcePool pool, string xmlPath, string xsdPath,string xmlxsdPath, IResourceBuilder builder)
        {
            // Set the validation settings.
            XmlSchemaSet schemaSet = new XmlSchemaSet();
            schemaSet.Add("", xsdPath);
            schemaSet.Add("http://www.w3.org/XML/1998/namespace", xmlxsdPath);

            XmlReader reader = XmlReader.Create(xmlPath);
            XDocument xmlDocument = XDocument.Load(reader);
            xmlDocument.Validate(schemaSet, ValidationCallBack);


            reader = XmlReader.Create(xmlPath);
            ConstructFromXML(pool, reader, builder);
        }

        private static void ValidationCallBack(object sender, ValidationEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        public static void ConstructFromXML(ResourcePool pool, XmlReader reader, IResourceBuilder builder) {
            
            while (reader.ReadToFollowing("resource"))
            {
                XmlDocument doc = new XmlDocument();
                XmlNode node = doc.ReadNode(reader);
                BasicResourceDirector.ConstructFromXmlNode(builder, node);

                Resource resource = builder.GetResource();
                pool.Add(resource);
            }
        }
    }
}
