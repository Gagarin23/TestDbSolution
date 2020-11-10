using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Xml.Serialization;

namespace EfProject.Model
{
    [XmlType("shop")]
    public partial class Shop
    {
        private Company _company;
        private string _shopId;
        private List<Currency> _currencies;
        private List<Category> _categories;
        private List<Offer> _offers;

        [Key]
        [XmlElement("name")]
        public string ShopId
        {
            get => _shopId;
            set
            {
                if(string.IsNullOrEmpty(value))
                    throw new ArgumentNullException();

                _shopId = value;
            }
        }

        [XmlElement("company")]
        public Company Company
        {
            get => _company;
            set
            {
                _company = value ?? throw new ArgumentNullException();
            }
        }

        [XmlElement("url")]
        public string Url { get; set; }

        [XmlIgnore]
        public ICollection<Currency> Currencies
        {
            get => _currencies;
            set => _currencies = value as List<Currency> ?? throw new ArgumentNullException();
        }

        [XmlIgnore]
        public ICollection<Category> Categories
        {
            get => _categories;
            set => _categories = value as List<Category> ?? throw new ArgumentNullException();
        }

        [XmlIgnore]
        public ICollection<Offer> Offers
        {
            get => _offers;
            set => _offers = value as List<Offer> ?? throw new ArgumentNullException();
        }

        public override string ToString()
        {
            return ShopId + "-" + Company.Name;
        }
    }

    partial class Shop
    {
        [XmlArray("currencies")]
        [NotMapped]
        public List<Currency> XmlCurrencies
        {
            get => _currencies;
            set => _currencies = value ?? throw new ArgumentNullException();
        }

        [XmlArray("categories")]
        [NotMapped]
        public List<Category> XmlCategories
        {
            get => _categories;
            set => _categories = value;
        }

        [XmlArray("offers")]
        [NotMapped]
        public List<Offer> XmlOffers
        {
            get => _offers;
            set => _offers = value;
        }
    }
}
