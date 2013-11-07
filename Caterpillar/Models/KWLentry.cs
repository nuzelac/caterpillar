using System;
using System.Collections.Generic;

namespace Caterpillar.Models
{
    public partial class KWLentry
    {
        public KWLentry()
        {
            this.IsResponseOns = new List<IsResponseOn>();
            this.IsResponseOns1 = new List<IsResponseOn>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual ICollection<IsResponseOn> IsResponseOns { get; set; }
        public virtual ICollection<IsResponseOn> IsResponseOns1 { get; set; }
        public virtual User User { get; set; }
    }
}
