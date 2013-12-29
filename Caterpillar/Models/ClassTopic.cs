using System;
using System.Collections.Generic;

namespace Caterpillar.Models
{
    public partial class ClassTopic
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public int TopicId { get; set; }
        public virtual Class Class { get; set; }
        public virtual Topic Topic { get; set; }
    }
}
