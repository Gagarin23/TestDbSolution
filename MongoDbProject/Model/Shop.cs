using System;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Xml.Serialization;
using MongoDB.Bson;

namespace MongoDbProject.Model
{
    [XmlType("shop")]
    public class Shop
    {
        private Company _company;
        private string _name;
        private List<Currency> _currencies = new List<Currency>();

        [BsonId]
        [XmlElement("name")]
        public string Name
        {
            get => _name;
            set
            {
                if(string.IsNullOrEmpty(value))
                    throw new ArgumentNullException();

                _name = value;
            }
        }

        [BsonIgnore]
        [XmlElement("company")]
        public Company Company
        {
            get => _company;
            set
            {
                _company = value ?? throw new ArgumentNullException();
                CompanyName = value.Name;
            }
        }

        /// <summary>
        /// Для установки значения используйте свойство Company. Публичный сеттер исключительно для десериализации XML!
        /// </summary>
        [BsonElement("Company")]
        public string CompanyName { get; set; }

        [XmlElement("url")]
        public string Url { get; set; }

        [BsonIgnore]
        [XmlArray("currencies")]
        public List<Currency> Currencies
        {
            get => _currencies;
            set => _currencies = value ?? throw new ArgumentNullException();
        }

        public List<string> CurrenciesId { get; set; } = new List<string>();

        [BsonIgnore]
        [XmlArray("categories")]
        public List<Category> Categories { get; set; } = new List<Category>();

        public List<int> CategoriesId { get; set; } = new List<int>();

        
        [XmlArray("offers")]
        public List<Offer> Offers { get; set; } = new List<Offer>();
        [BsonIgnore]
        public List<int> OffersId { get; set; } = new List<int>();

        public void SetIds()
        {
            CurrenciesId.AddRange(Currencies?.Select(c => c.Name) ?? Array.Empty<string>());
            CategoriesId.AddRange(Categories?.Select(c => c.Id) ?? Array.Empty<int>());
            OffersId.AddRange(Offers?.Select(o => o.Id) ?? Array.Empty<int>());
        }

        public override string ToString()
        {
            return Name + "-" + CompanyName;
        }
    }
}
