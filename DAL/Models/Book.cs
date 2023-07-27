using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Book
    {
        public int Id { get; set; }
        public string Isbn { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public decimal Price { get; set; }
        public string LocationName { get; set; } = null!;
        public int PressId { get; set; }

        public virtual Address LocationNameNavigation { get; set; } = null!;
        public virtual Press Press { get; set; } = null!;
    }
}
