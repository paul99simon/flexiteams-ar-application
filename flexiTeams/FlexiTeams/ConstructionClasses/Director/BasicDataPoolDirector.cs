using FlexiTeams.ConstructionClasses.Builder;
using FlexiTeams.ConstructionClasses.Diretor;
using FlexiTeams.DataClasses.Data;
using FlexiTeams.DataClasses.Resource;
using FlexiTeams.Exceptions;
using FlexiTeams.Inventory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace FlexiTeams.ConstructionClasses.Director
{
    public class BasicDataPoolDirector
    {
        public static void ConstructFromXml(DataPool pool, string xmlPath)
        {
            var builder = new BasicDataBuilder();
            var setting = GetXmlReaderSettings(GetSchemaLocations(xmlPath));
            using var reader = XmlReader.Create(xmlPath, setting);

            ConstructFromXml(pool, reader, builder);
        }
        public static void ConstructFromXml(DataPool pool, string xmlPath, IDataBuilder builder)
        {
            var setting = GetXmlReaderSettings(GetSchemaLocations(xmlPath));
            using var reader = XmlReader.Create(xmlPath, setting);

            ConstructFromXml(pool, reader, builder);
        }
        public static DataPool ConstructFromXml(string xmlPath)
        {
            var pool = new DataPool();
            var builder = new BasicDataBuilder();
            var setting = GetXmlReaderSettings(GetSchemaLocations(xmlPath));
            using var reader = XmlReader.Create(xmlPath, setting);

            ConstructFromXml(pool, reader, builder);
            return pool;
        }
        public static DataPool ConstructFromXml(string xmlPath, IDataBuilder builder)
        {
            var pool = new DataPool();
            var setting = GetXmlReaderSettings(GetSchemaLocations(xmlPath));
            using var reader = XmlReader.Create(xmlPath, setting);

            ConstructFromXml(pool, reader, builder);
            return pool;
        }

        private static void ConstructFromXml(DataPool pool, XmlReader reader, IDataBuilder builder)
        {
            while (reader.ReadToFollowing("Data"))
            {
                XmlDocument doc = new XmlDocument();
                XmlNode node = doc.ReadNode(reader);
                BasicDataDirector.ConstructFromXmlNode(builder, node);

                Data data = builder.GetData();
                pool.Add(data);
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
            if (nameSpace == null) throw new InvalidXmlInstanceException("'namespace' atribute is not declared");

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
