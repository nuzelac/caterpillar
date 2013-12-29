using System;
using System.Collections.Generic;

namespace Caterpillar.Models
{
    public partial class User
    {
        public User()
        {
            this.KWLentries = new List<KWLentry>();
            this.Responses = new List<Response>();
            this.UserClassCourses = new List<UserClassCourse>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public virtual ICollection<KWLentry> KWLentries { get; set; }
        public virtual ICollection<Response> Responses { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<UserClassCourse> UserClassCourses { get; set; }
    }
}
