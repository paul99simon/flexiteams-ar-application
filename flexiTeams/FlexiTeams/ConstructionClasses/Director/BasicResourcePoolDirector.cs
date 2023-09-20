using FlexiTeams.ConstructionClasses.Builder;
using FlexiTeams.ConstructionClasses.Diretor;
using FlexiTeams.DataClasses.Resource;
using FlexiTeams.Exceptions;
using System.Xml;
using System.Xml.Schema;

namespace FlexiTeams.ConstructionClasses.Director
{
    public class BasicResourcePoolDirector
    {
        /// <summary>
        /// This method adds data from a xml file to a ResourcePool <see cref="ResourcePool"/>.
        /// The xml file is validated against the specified xsd schema
        /// How the resources <see cref="Resource"/> are constructed are specified in the BasicResourceBuilder <see cref="BasicResourceBuilder"/>.
        /// </summary>
        /// <param name="pool"></param>
        /// <param name="xmlPath">path to xml file</param>
        /// <exception cref="InvalidXmlInstanceException"></exception>
        public static void ConstructFromXml(ResourcePool pool, string xmlPath)
        {

            var setting = GetXmlReaderSettings(GetSchemaLocations(xmlPath));
            var builder = new BasicResourceBuilder();
            using var reader = XmlReader.Create(xmlPath, setting);
            ConstructFromXml(pool, reader, builder);
        }
        
        /// <summary>
        /// This method adds data from a xml file to a ResourcePool <see cref="ResourcePool"/>.
        /// The xml file is validated against the specified xsd schema
        /// How the resources <see cref="Resource"/> are constructed are specified in the ResourceBuilder <see cref="IResourceBuilder"/>.
        /// </summary>
        /// <param name="pool"></param>
        /// <param name="xmlPath">path to xml file</param>
        /// <param name="builder">ResourceBuilder which specifies how a resource is constructed</param>
        /// <exception cref="InvalidXmlInstanceException"></exception>
        public static void ConstructFromXml(ResourcePool pool, string xmlPath, IResourceBuilder builder) {

            var setting = GetXmlReaderSettings(GetSchemaLocations(xmlPath));
            using var reader = XmlReader.Create(xmlPath, setting);
            ConstructFromXml(pool, reader, builder);
        }

        /// <summary>
        /// This method constructs a resource pool <see cref="ResourcePool"/> from xml file data.
        /// The xml file is validated against the specified xsd schema
        /// How the resources <see cref="Resource"/> are constructed are specified in the ResourceBuilder <see cref="IResourceBuilder"/>
        /// </summary>
        /// <param name="xmlPath">path to xml file</param>
        /// <param name="builder">ResourceBuilder which specifies how a resource is constructed</param>
        /// <returns>Resourcepool which contains the data from the xml file</returns>
        /// <exception cref="InvalidXmlInstanceException"></exception>
        public static ResourcePool ConstructFromXml(string xmlPath, IResourceBuilder builder)
        {
            var pool = new ResourcePool();
            var setting = GetXmlReaderSettings(GetSchemaLocations(xmlPath));
            using var reader = XmlReader.Create(xmlPath, setting);
            ConstructFromXml(pool, reader, builder);

            return pool;

        }

        /// <summary>
        /// This method constructs a resource pool <see cref="ResourcePool"/> from xml file data.
        /// The xml file is validated against the specified xsd schema
        /// How the resources <see cref="Resource"/> are constructed are specified in the BasicResourceBuilder <see cref="BasicResourceBuilder"/>.
        /// </summary>
        /// <param name="xmlPath">path to xml file</param>
        /// <returns>Resourcepool which contains the data from the xml file</returns>
        /// <exception cref="InvalidXmlInstanceException"></exception>
        public static ResourcePool ConstructFromXml(string xmlPath)
        {
            var pool = new ResourcePool();
            var setting = GetXmlReaderSettings(GetSchemaLocations(xmlPath));
            using var reader = XmlReader.Create(xmlPath, setting);
            var builder = new BasicResourceBuilder();
            ConstructFromXml(pool, reader, builder);

            return pool;
        }

        //utility methods
        private static void ConstructFromXml(ResourcePool pool, XmlReader reader, IResourceBuilder builder)
        {
            while (reader.ReadToFollowing("Resource"))
            {
                XmlDocument doc = new XmlDocument();
                XmlNode node = doc.ReadNode(reader);
                BasicResourceDirector.ConstructFromXmlNode(builder, node);

                Resource resource = builder.GetResource();
                pool.Add(resource);
            }
        }
        private static void ValidationCallBack(object sender, ValidationEventArgs e)
        {
            throw new InvalidXmlInstanceException(e.Message);
        }
        private static List<KeyValuePair<string, string>> GetSchemaLocations(string xmlPath)
        {
            List<KeyValuePair<string, string>> list = new();
            var pair1 = GetNoNamespaceSchemaLocation(xmlPath);
            var pair2 = GetSchemaLocation(pair1.Value);
            list.Add(pair1);
            list.Add(pair2);
            return list;
        }
        private static KeyValuePair<string, string> GetNoNamespaceSchemaLocation(string xmlPath)
        { 
        using XmlReader reader = XmlReader.Create(xmlPath);
        XmlDocument document = new XmlDocument();
        document.Load(reader);

        XmlNode root = document.DocumentElement;
        string path = root.Attributes["xsi:noNamespaceSchemaLocation"].Value;

        if (path is null) throw new InvalidXmlInstanceException("'noNamespaceSchemaLocation' atribute is not declared");
        return new KeyValuePair<string, string>("", GetParentDirectory(xmlPath) + "/" + path);
    }
        private static KeyValuePair<string, string> GetSchemaLocation(string xmlPath)
        {
            using XmlReader reader = XmlReader.Create(xmlPath);
            XmlDocument document = new XmlDocument();
            document.Load(reader);

            var nameSpaceManager = new XmlNamespaceManager(document.NameTable);
            nameSpaceManager.AddNamespace("xs", "http://www.w3.org/2001/XMLSchema");

            XmlNode importNode = document.SelectSingleNode("//xs:import", nameSpaceManager);
            string path = importNode.Attributes["schemaLocation"].Value;
            string nameSpace = importNode.Attributes["namespace"].Value;

            if (path is null) throw new InvalidXmlInstanceException("'schemaLocation' atribute is not declared");
            if(nameSpace == null) throw new InvalidXmlInstanceException("'namespace' atribute is not declared");

            return new KeyValuePair<string, string>(nameSpace, GetParentDirectory(xmlPath) + "/" + path);
        }
        private static void ValidationHandler(object sender, ValidationEventArgs e)
        {
            throw new InvalidXmlInstanceException(e.Message);
        }
        private static XmlReaderSettings GetXmlReaderSettings(List<KeyValuePair<string, string>> schemaLocations)
        {
            XmlSchemaSet schemaSet = new XmlSchemaSet();
            schemaLocations.ForEach(pair => schemaSet.Add(pair.Key, pair.Value));
            schemaSet.Compile();

            var settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
            settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);
            settings.Schemas = schemaSet;

            return settings;
        }
        private static string GetParentDirectory(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            return di.Parent.FullName;
        }
    }
}
