using System;
using System.Collections.Generic;

namespace Caterpillar.Models
{
    public partial class CourseTopic
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int TopicId { get; set; }
        public virtual Course Course { get; set; }
        public virtual Topic Topic { get; set; }
    }
}
