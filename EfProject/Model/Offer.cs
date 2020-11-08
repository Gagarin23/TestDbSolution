using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using EfProject.BL.MSSQL;

namespace EfProject.Model
{
    [XmlType("offer")]
    public class Offer
    {
        private int _id;
        private List<Category> _categories;
        private Currency _currency;
        private decimal _basePrice;
        private int _count;
        private Vendor _vendor;
        private string _name;
        private int _groupId;
        private string _vendorName;
        private OfferGroup _offerGroup;
        private List<int> _categoryId = new List<int>();

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
        [NotMapped]
        public int GroupId
        {
            get => _groupId;
            set
            {
                if(value < 1)
                    throw new ArgumentException();

                using (var db = new TestDbContext())
                {
                    var group = db.OfferGroups.Find(value);

                    if(group == null)
                        group = new OfferGroup(){Id = value};

                    _offerGroup = group;
                    _groupId = value;
                }
            }
        }

        public OfferGroup OfferGroup
        {
            get => _offerGroup;
            set
            {
                if(value == null)
                    throw new ArgumentNullException();

                _offerGroup = value;
                _groupId = value.Id;
            }
        }

        /// <summary>
        /// Для установки значения используйте свойство ParentCategory. Публичный сеттер исключения для десериализации XML!
        /// </summary>
        [XmlElement("categoryId")]
        [NotMapped]
        public List<int> CategoryId
        {
            get => _categoryId;
            set
            {
                if(value == null)
                    throw new ArgumentNullException();

                using (var db = new TestDbContext())
                {
                    foreach (var nameId in value)
                    {
                        var category = db.Categories.Find(nameId) ?? new Category() {Id = nameId};
                        _categories.Add(category);
                    }
                }
            }
        }
        
        public List<Category> Category
        {
            get => _categories;
            set => _categories = value;
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
        [XmlElement("currencyId")]
        [NotMapped]
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
        [NotMapped]
        public string PictureUrl { get; set; }

        [XmlElement("delivery")]
        public bool IsDelivery { get; set; }

        [XmlArray("orderingTime")]
        [XmlArrayItem("ordering")]
        [NotMapped]
        public List<string> Locations { get; set; } = new List<string>();

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
        [XmlElement("vendor")]
        [NotMapped]
        public string VendorName
        {
            get => _vendorName;
            set
            {
                using (var db = new TestDbContext())
                {
                    var vendor = db.Vendors.Find(value);

                    if (vendor == null)
                        vendor = new Vendor() { Name = value };

                    _vendor = vendor;
                    _vendorName = value;
                }
            }
        }

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
        [NotMapped]
        public List<AdditionalDescriptions> AdditionalDescriptions { get; set; } = new List<AdditionalDescriptions>();

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

    [XmlType("param")]
    public class AdditionalDescriptions
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlText]
        public string Description { get; set; }
    }
}
