using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbProject.Model
{
    [XmlType("company")]
    public class Company
    {
        private string _name;

        [BsonId]
        [XmlText]
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException();

                _name = value;
            }
        }

        [XmlElement("shops")]
        public List<Shop> Shops { get; set; } = new List<Shop>();
        public override string ToString()
        {
            return Name;
        }
    }
}
