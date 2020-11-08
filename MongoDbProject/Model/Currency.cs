using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbProject.Model
{
    [XmlType("currency")]
    public class Currency
    {
        private string _name;
        private decimal _rate;

        [BsonId]
        [XmlAttribute("id")]
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

        /// <summary>
        /// Коэффициент к рублю? 
        /// </summary>
        [XmlAttribute("rate")]
        public decimal Rate
        {
            get => _rate;
            set
            {
                if(value <= 0)
                    throw new ArgumentNullException();

                _rate = value;
            }
        }
        
        public override string ToString()
        {
            return Name;
        }
    }
}
