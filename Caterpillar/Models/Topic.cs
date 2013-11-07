using System;
using System.Collections.Generic;

namespace Caterpillar.Models
{
    public partial class Topic
    {
        public Topic()
        {
            this.CourseTopics = new List<CourseTopic>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CourseId { get; set; }
        public virtual ICollection<CourseTopic> CourseTopics { get; set; }
    }
}
