using System;
using System.Collections.Generic;

namespace Caterpillar.Models
{
    public partial class Class
    {
        public Class()
        {
            this.UserClassCourses = new List<UserClassCourse>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public virtual ICollection<UserClassCourse> UserClassCourses { get; set; }
    }
}
