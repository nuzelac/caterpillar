using System;
using System.Collections.Generic;

namespace Caterpillar.Models
{
    public partial class Topic
    {
        public Topic()
        {
            this.ClassTopics = new List<ClassTopic>();
            this.CourseTopics = new List<CourseTopic>();
            this.KWLentries = new List<KWLentry>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<int> CreatedByUserId { get; set; }
        public Nullable<int> LastEditedByUserId { get; set; }
        public Nullable<System.DateTime> LastEditDate { get; set; }
        public virtual ICollection<ClassTopic> ClassTopics { get; set; }
        public virtual ICollection<CourseTopic> CourseTopics { get; set; }
        public virtual ICollection<KWLentry> KWLentries { get; set; }
    }
}
