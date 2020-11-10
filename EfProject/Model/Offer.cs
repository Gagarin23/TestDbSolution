using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Xml.Serialization;
using EfProject.BL.MSSQL;
using Microsoft.EntityFrameworkCore;

namespace EfProject.Model
{
    [XmlType("offer")]
    public partial class Offer
    {
        private int _id;
        private ICollection<Category> _categories = new List<Category>();
        private Currency _currency;
        private decimal _basePrice;
        private int _count;
        private string _vendor;
        private string _name;
        private int _groupId;
        private List<int> _categoryId = new List<int>();
        private string _currencyId;

        [Key]
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
        public bool IsAvaiblable { get; set; }

        [XmlAttribute("group_id")]
        public int GroupId
        {
            get => _groupId;
            set
            {
                if(value < 1)
                    throw new ArgumentException();

                _groupId = value;
                
            }
        }

        [XmlIgnore]
        public ICollection<Category> Categories
        {
            get => _categories;
            set
            {
                _categories = value ?? throw new ArgumentNullException();
                _categoryId.AddRange(value.Select(c => c.Id));
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
        
        public Currency Currency
        {
            get => _currency;
            set
            {
                _currency = value ?? throw new ArgumentNullException();
                _currencyId = value.Name;
                Price = BasePrice * value.Rate;
            }
        }

        [XmlElement("delivery")]
        public bool IsDelivery { get; set; }

        [XmlElement("vendor")]
        public string Vendor
        {
            get => _vendor;
            set => _vendor = value ?? throw new ArgumentNullException();
        }

        /// <summary>
        /// Ограничение по возрасту.
        /// </summary>
        [XmlElement("age")]
        public string Age { get; set; }

        [XmlElement("barcode")]
        public long Barcode { get; set; }
    }

    partial class Offer
    {
        /// <summary>
        /// Для установки значения используйте свойство ParentCategory. Публичный сеттер исключения для десериализации XML!
        /// </summary>
        [XmlElement("categoryId")]
        [NotMapped]
        public List<int> CategoryId
        {
            get => _categoryId;
            set => _categoryId = value ?? throw new ArgumentNullException();
        }

        /// <summary>
        /// Для установки значения используйте свойство Currency. Публичный сеттер исключительно для десериализации XML!
        /// </summary>
        [XmlElement("currencyId")]
        [NotMapped]
        public string CurrencyId
        {
            get => _currencyId;
            set => _currencyId = value ?? throw new ArgumentNullException();
        }

        [XmlElement("description")]
        [NotMapped]
        public string Description { get; set; }

        [XmlElement("param")]
        [NotMapped]
        public List<AdditionalDescriptions> AdditionalDescriptions { get; set; } = new List<AdditionalDescriptions>();

        [XmlArray("orderingTime")]
        [XmlArrayItem("ordering")]
        [NotMapped]
        public List<string> Locations { get; set; } = new List<string>();

        [XmlElement("picture")]
        [NotMapped]
        public string PictureUrl { get; set; }
    }

    [XmlType("param")]
    public class AdditionalDescriptions
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlText]
        public string Description { get; set; }
    }

    partial class Offer
    {
        public void ChangeCount(int value)
        {
            _count += value;

            if (_count < 0)
                throw new Exception("Недостаточно единиц товара");

            IsAvaiblable = _count > 0; //Как мне кажется так должно реализовываться свойство.
            if (IsAvaiblable)
                Locations.Clear();
        }

        public int Count() => _count;
    }
}
