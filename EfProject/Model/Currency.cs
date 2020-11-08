using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace EfProject.Model
{
    [XmlType("currency")]
    public class Currency
    {
        private string _name;
        private decimal _rate;

        [Key]
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
