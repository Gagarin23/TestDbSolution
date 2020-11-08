using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbProject.Model
{
    [XmlType("offer")]
    public class Offer
    {
        private int _id;
        private Category _category;
        private Currency _currency;
        private decimal _basePrice;
        private int _count;
        private Vendor _vendor;
        private string _name;

        [BsonId]
        [XmlAttribute("id")]
        public int Id
        {
            get => _id;
            set
            {
                if(value < 1)
                    throw new ArgumentException();

                _id = value;
            }
        }

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

        /// <summary>
        /// Для установки значения используйте метод ChangeCount. Публичный сеттер исключения для десериализации XML!
        /// </summary>
        [XmlAttribute("available")]
        public bool Avaiblable { get; set; }

        [BsonIgnoreIfNull]
        [XmlAttribute("group_id")]
        public int GroupId { get; set; }

        /// <summary>
        /// Для установки значения используйте свойство ParentCategory. Публичный сеттер исключения для десериализации XML!
        /// </summary>
        [XmlElement("categoryId")] 
        public List<int> CategoryId { get; set; } = new List<int>();

        [BsonIgnore]
        public Category Category
        {
            get => _category;
            set
            {
                _category = value;
                CategoryId.Add(value.Id);
            }
        }

        [XmlElement("url")]
        public string Url { get; set; }

        /// <summary>
        /// Для установки значения используйте свойство Currency. Публичный сеттер исключительно для десериализации XML!
        /// </summary>
        [XmlElement("price")]
        public decimal Price { get; set; }

        [XmlElement("baseprice")]
        public decimal BasePrice
        {
            get => _basePrice;
            set
            {
                if(value <= 0)
                    throw new ArgumentException();

                _basePrice = value;
            }
        }

        /// <summary>
        /// Для установки значения используйте свойство Currency. Публичный сеттер исключительно для десериализации XML!
        /// </summary>
        [BsonIgnore]
        [XmlElement("currencyId")]
        public string CurrencyId { get; set; }
        
        public Currency Currency
        {
            get => _currency;
            set
            {
                _currency = value ?? throw new ArgumentNullException();
                Price = BasePrice * value.Rate;
            }
        }

        [XmlElement("picture")]
        public string PictureUrl { get; set; }

        [XmlElement("delivery")]
        public bool IsDelivery { get; set; }

        [XmlArray("orderingTime")]
        [XmlArrayItem("ordering")]
        public List<string> Location { get; set; } = new List<string>();

        [BsonIgnore]
        [XmlElement("vendor")]
        public Vendor Vendor
        {
            get => _vendor;
            set
            {
                _vendor = value ?? throw new ArgumentNullException();
                VendorName = value.Name;
            }
        }

        /// <summary>
        /// Для установки значения используйте свойство Vendor. Публичный сеттер исключительно для десериализации XML!
        /// </summary>
        [BsonElement("Vendor")]
        public string VendorName { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        /// <summary>
        /// Ограничение по возрасту.
        /// </summary>
        [XmlElement("age")]
        public string Age { get; set; }

        [XmlElement("barcode")]
        public long Barcode { get; set; }

        [XmlElement("param")]
        public List<AdditionalDescriptions> AdditionalDescriptions { get; set; } = new List<AdditionalDescriptions>();

        public void ChangeCount(int value)
        {
            _count += value;

            if (_count < 0)
                throw new Exception("Недостаточно единиц товара");

            Avaiblable = _count > 0; //Как мне кажется так должно реализовываться свойство.
        }

        public int Count() => _count;
    }

    [XmlType("param")]
    public class AdditionalDescriptions
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlText]
        public string Description { get; set; }
    }
}
