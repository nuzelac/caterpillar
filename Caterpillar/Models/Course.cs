using System;
using System.Collections.Generic;

namespace Caterpillar.Models
{
    public partial class Course
    {
        public Course()
        {
            this.CourseTopics = new List<CourseTopic>();
            this.UserClassCourses = new List<UserClassCourse>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<CourseTopic> CourseTopics { get; set; }
        public virtual ICollection<UserClassCourse> UserClassCourses { get; set; }
    }
}
