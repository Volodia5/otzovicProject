using System;
using System.Collections.Generic;

namespace ResponcesOnline.Model
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public byte[]? Image { get; set; }
        public int? CategoryId { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
