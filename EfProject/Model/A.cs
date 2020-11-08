using System;
using System.Collections.Generic;
using System.Text;

namespace EfProject.Model
{
    class A
    {
        public int Id { get; set; }
        public ICollection<B> Collection { get; set; } = new List<B>();
    }
}
