using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Address
    {
        public Address()
        {
            Books = new HashSet<Book>();
        }

        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;

        public virtual ICollection<Book> Books { get; set; }
    }
}
