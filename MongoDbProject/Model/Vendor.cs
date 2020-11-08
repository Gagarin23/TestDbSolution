using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbProject.Model
{
    [XmlType("vendor")]
    public class Vendor
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

        [XmlArray("offers")]
        public List<Offer> Offers { get; set; } = new List<Offer>();
        public override string ToString()
        {
            return Name;
        }
    }
}
