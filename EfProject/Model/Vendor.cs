using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace EfProject.Model
{
    [XmlType("vendor")]
    public class Vendor
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

        [XmlArray("offers")]
        public List<Offer> Offers { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
