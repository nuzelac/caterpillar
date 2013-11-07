using System;
using System.Collections.Generic;

namespace Caterpillar.Models
{
    public partial class IsResponseOn
    {
        public int Id { get; set; }
        public int EntryId { get; set; }
        public int ResponseId { get; set; }
        public virtual KWLentry KWLentry { get; set; }
        public virtual KWLentry KWLentry1 { get; set; }
    }
}
