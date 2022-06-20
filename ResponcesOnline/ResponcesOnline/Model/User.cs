using System;
using System.Collections.Generic;

namespace ResponcesOnline.Model
{
    public partial class User
    {
        public User()
        {
            Reports = new HashSet<Report>();
        }

        public int Id { get; set; }
        public string Nickname { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int RoleId { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<Report> Reports { get; set; }


    }
}
