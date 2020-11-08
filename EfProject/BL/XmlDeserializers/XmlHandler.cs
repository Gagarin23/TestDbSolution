using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using EfProject.Controller;

namespace EfProject.BL.XmlDeserializers
{
    class XmlHandler : IXmlGetElement
    {
        public Exception Exception { get; set; }
        public string Address { get; set; }
        public XmlReaderSettings XmlReaderSettings { get; set; } = new XmlReaderSettings()
        {
            Async = true,
            DtdProcessing = DtdProcessing.Parse,
            ValidationType = ValidationType.Schema
        };

        public XmlHandler(string address)
        {
            if(string.IsNullOrEmpty(address))
                throw new ArgumentNullException();

            Address = address;
        }

        /// <summary>
        /// Получить объект по имени из xml-документа. Если имя не найдено, то вернёт NULL !!
        /// </summary>
        public T GetElement<T>(string searchingElement) where T : class
        {
            try
            {
                using (XmlReader reader = XmlReader.Create(Address, XmlReaderSettings))
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == searchingElement)
                        {
                            var xml = reader.ReadOuterXml();
                            var serializer = new XmlSerializer(typeof(T));

                            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
                            {
                                return (T)serializer.Deserialize(ms); // Запаковка в object при возврате крайне не радует :(
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Exception = e;
                return default;
            }

            return default;
        }
    }
}
