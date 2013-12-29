using System;
using System.Collections.Generic;

namespace Caterpillar.Models
{
    public partial class KWLentry
    {
        public KWLentry()
        {
            this.Responses = new List<Response>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int Type { get; set; }
        public string Entry { get; set; }
        public Nullable<int> TopicId { get; set; }
        public virtual ICollection<Response> Responses { get; set; }
        public virtual Topic Topic { get; set; }
        public virtual User User { get; set; }
    }
}
