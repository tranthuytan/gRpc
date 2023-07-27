using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Press
    {
        public Press()
        {
            Books = new HashSet<Book>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Category { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
