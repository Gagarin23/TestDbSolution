using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EfProject.Model
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        public string InShop { get; set; }
        public string InWarehouse { get; set; }
    }
}
