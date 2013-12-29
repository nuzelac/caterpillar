using System;
using System.Collections.Generic;

namespace Caterpillar.Models
{
    public partial class Response
    {
        public int Id { get; set; }
        public int EntryId { get; set; }
        public int UserId { get; set; }
        public string Response1 { get; set; }
        public Nullable<int> Correction { get; set; }
        public Nullable<int> Points { get; set; }
        public virtual KWLentry KWLentry { get; set; }
        public virtual User User { get; set; }
    }
}
