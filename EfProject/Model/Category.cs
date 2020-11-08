using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using EfProject.BL.MSSQL;

namespace EfProject.Model
{
    [XmlType("category")]
    public class Category
    {
        private int _id;
        private Category _category;
        private string _name;
        private int _parentId;

        [Key]
        [XmlAttribute("id")]
        public int Id
        {
            get => _id;
            set
            {
                if(value < 1)
                    throw new ArgumentNullException();

                _id = value;
            }
        }

        [XmlText]
        public string Name
        {
            get => _name;
            set => _name = value ?? throw new ArgumentNullException();
        }

        /// <summary>
        /// Для установки значения используйте свойство ParentCategory. Публичный сеттер исключительно для десериализации XML!
        /// </summary>
        [XmlAttribute("parentId")]
        [NotMapped]
        public int ParentId
        {
            get => _parentId;
            set
            {
                if(value < 1)
                    throw  new ArgumentException();

                using (var db = new TestDbContext())
                {
                    var category = db.Categories.Find(value);

                    if (category == null) 
                        category = new Category() {Id = value};

                    _category = category;
                    _parentId = value;
                }
            }
        }

        public Category ParentCategory
        {
            get => _category;
            set
            {
                _category = value;
                ParentId = value.Id;
            }
        }

        public override string ToString()
        {
            return Name + ", id: " + _id;
        }
    }
}
