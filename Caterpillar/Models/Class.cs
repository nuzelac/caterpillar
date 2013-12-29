using System;
using System.Collections.Generic;

namespace Caterpillar.Models
{
    public partial class Class
    {
        public Class()
        {
            this.ClassTopics = new List<ClassTopic>();
            this.UserClassCourses = new List<UserClassCourse>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public virtual ICollection<ClassTopic> ClassTopics { get; set; }
        public virtual ICollection<UserClassCourse> UserClassCourses { get; set; }
    }
}
