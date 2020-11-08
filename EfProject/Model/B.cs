using System;
using System.Collections.Generic;
using System.Text;

namespace EfProject.Model
{
    class B
    {
        public int Id { get; set; }
        public ICollection<A> Collection { get; set; } = new List<A>();
    }
}
