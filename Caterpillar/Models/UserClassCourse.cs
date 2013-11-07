using System;
using System.Collections.Generic;

namespace Caterpillar.Models
{
    public partial class UserClassCourse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ClassId { get; set; }
        public int CourseId { get; set; }
        public virtual Class Class { get; set; }
        public virtual Course Course { get; set; }
        public virtual User User { get; set; }
    }
}
