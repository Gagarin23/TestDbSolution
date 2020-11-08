using System.Xml;

namespace MongoDbProject.Controller
{
    internal interface IXmlGetElement
    {
        string Address { get; set; }
        XmlReaderSettings XmlReaderSettings { get; set; }

        /// <summary>
        /// Получить объект по имени из xml-документа. Если имя не найдено, то вернёт NULL !!
        /// </summary>
        T GetElement<T>(string searchingElement) where T : class;
    }
}