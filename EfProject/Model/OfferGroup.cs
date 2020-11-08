using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EfProject.Model
{
    public class OfferGroup
    {
        [Key]
        public int Id { get; set; }

        public List<Offer> Offers { get; set; }
    }
}
