using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbProject.Model
{
    [XmlType("category")]
    public class Category
    {
        private int _id;
        private Category _category;
        private string _name;

        [BsonId]
        [XmlAttribute("id")]
        public int Id
        {
            get => _id;
            set
            {
                if(value < 1)
                    throw new ArgumentNullException();

                _id = value;
            }
        }

        [XmlText]
        public string Name
        {
            get => _name;
            set => _name = value ?? throw new ArgumentNullException();
        }

        /// <summary>
        /// Для установки значения используйте свойство ParentCategory. Публичный сеттер исключительно для десериализации XML!
        /// </summary>
        [BsonIgnoreIfDefault]
        [XmlAttribute("parentId")]
        public int ParentId { get; set; }

        [BsonIgnore]
        public Category ParentCategory
        {
            get => _category;
            set
            {
                _category = value;
                ParentId = value.Id;
            }
        }

        public override string ToString()
        {
            return Name + ", id: " + _id;
        }
    }
}
