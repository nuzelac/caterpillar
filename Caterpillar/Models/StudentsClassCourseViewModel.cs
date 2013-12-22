using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Caterpillar.Models
{
    public class StudentsClassCourseViewModel
    {
        public User User { get; set; }
        public Class Class { get; set; }
        public Course Course { get; set; }
    }
}