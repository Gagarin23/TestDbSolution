using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Serialization;

namespace EfProject.Model
{
    [XmlType("shop")]
    public class Shop
    {
        private Company _company;
        private string _name;
        private List<Currency> _currencies;

        [Key]
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

        [XmlArray("currencies")]
        public List<Currency> Currencies
        {
            get => _currencies;
            set => _currencies = value ?? throw new ArgumentNullException();
        }

        [XmlArray("categories")]
        public List<Category> Categories { get; set; }

        [XmlArray("offers")]
        public List<Offer> Offers { get; set; }

        public override string ToString()
        {
            return Name + "-" + Company.Name;
        }
    }
}
