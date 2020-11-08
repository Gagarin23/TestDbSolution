using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace EfProject.Model
{
    [XmlType("company")]
    public class Company
    {
        private string _name;

        [Key]
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

        public List<Offer> Offers { get; set; }
        public List<Shop> Shops { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
