using System;
using System.Collections.Generic;

namespace ResponcesOnline.Model
{
    public partial class Product
    {
        public Product()
        {
            Reports = new HashSet<Report>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public int? AvgRating { get; set; }
        public byte[]? Image { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<Report> Reports { get; set; }
    }
}
