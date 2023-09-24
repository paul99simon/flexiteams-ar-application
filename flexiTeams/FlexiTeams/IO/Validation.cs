using System.Net;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace FlexiTeams.IO
{
    public class Validation
    {
        /// <summary>
        /// takes a path for an xml document and validates it against its specified schema
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <returns></returns>
        /// <exception cref="XmlSchemaValidationException"/>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="XmlSchemaException"></exception>
        public static XDocument Validate(string xmlPath)
        {
            CheckNullOrEmpty(xmlPath);
            CheckFileExists(xmlPath);

            XDocument xDocument = XDocument.Load(xmlPath);

            var schemaFileName = GetParentSchemaFileName(xDocument);
            string schemaFileLocation = Path.GetDirectoryName(xmlPath);

            var schemaSet = GetSchemas(schemaFileLocation, schemaFileName);

            Validate(xDocument, schemaSet);
            return xDocument;
        }

        /// <summary>
        /// Validates a <see cref="XDocument" against a <see cref="XmlSchemaSet"/>/>
        /// </summary>
        /// <param name="xDocument"></param>
        /// <param name="schemaSet"></param>
        /// <exception cref="XmlSchemaValidationException"></exception>
        public static void Validate(XDocument xDocument, XmlSchemaSet schemaSet)
        {
            xDocument.Validate(schemaSet, (o, e) =>
            {
                throw new XmlSchemaValidationException(e.Message);
            });
        }

        /// <summary>
        /// checks íf string is null or empty
        /// </summary>
        /// <param name="path"></param>
        /// <exception cref="ArgumentNullException"></exception>
        private static void CheckNullOrEmpty(string path)
        {
            if (path == null || path == string.Empty) throw new ArgumentNullException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <exception cref="FileNotFoundException"></exception>
        private static void CheckFileExists(string path)
        {
            if(! File.Exists(path)) throw new FileNotFoundException();
        }

        /// <summary>
        /// Takes an <see cref="XDocument" and extracts its 'noNamespaceSchemaLocation' attribute/>
        /// </summary>
        /// <param name="xDocument"></param>
        /// <returns></returns>
        /// <exception cref="XmlSchemaException"></exception>
        private static string GetParentSchemaFileName(XDocument xDocument)
        {
            var xmlnsAttr = xDocument.Root.Attribute(XNamespace.Xmlns + "xsi");

            if (xmlnsAttr == null) throw new XmlSchemaException("'xmlns:xsi' Attribute not declared");

            string xmlns = xmlnsAttr.Value;

            XNamespace xsi = XNamespace.Get(xmlns);

            var schemaLocationAttr = xDocument.Root.Attribute(xsi + "noNamespaceSchemaLocation");
            if (schemaLocationAttr == null) throw new XmlSchemaException("'xsi:noNamespaceSchemaLocation' Attribute not declared");
            
            return schemaLocationAttr.Value;
        }

        /// <summary>
        /// creates a <see cref="XmlSchemaSet" from the parent schema and all its includes if declared/>
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <param name="parentSchemaPath"></param>
        /// <returns></returns>
        private static XmlSchemaSet GetSchemas(string directoryPath, string parentSchemaPath)
        {
            List<string> schemaNames = new List<string>()
            {
                parentSchemaPath
            };

            var schemaDocument = XDocument.Load(directoryPath + "/" + parentSchemaPath);

            var xs = XNamespace.Get("http://www.w3.org/2001/XMLSchema");
            var includes = schemaDocument.Descendants(xs + "include");

            foreach ( var include in includes )
            {
                schemaNames.Add(include.Attribute("schemaLocation").Value);
            }
            
            XmlSchemaSet schemaSet = new XmlSchemaSet();
            schemaNames.ForEach(schema =>
            {
                schemaSet.Add("", directoryPath + "/" + schema);
            });

            return schemaSet;
        }
    }
}