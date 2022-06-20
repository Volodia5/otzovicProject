using System;
using System.Collections.Generic;

namespace ResponcesOnline.Model
{
    public partial class Report
    {
        public int Id { get; set; }
        public string Annotation { get; set; } = null!;
        public string GeneralImpression { get; set; } = null!;
        public string Pluses { get; set; } = null!;
        public string Minuses { get; set; } = null!;
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string AcquisitionMethod { get; set; } = null!;
        public string Recomendation { get; set; } = null!;
        public int Rating { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
