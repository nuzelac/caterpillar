using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Caterpillar.Models.Mapping;

namespace Caterpillar.Models
{
    public partial class CaterpillarContext : DbContext
    {
        static CaterpillarContext()
        {
            Database.SetInitializer<CaterpillarContext>(null);
        }

        public CaterpillarContext()
            : base("Name=CaterpillarContext")
        {
        }

        public DbSet<Class> Classes { get; set; }
        public DbSet<ClassTopic> ClassTopics { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseTopic> CourseTopics { get; set; }
        public DbSet<KWLentry> KWLentries { get; set; }
        public DbSet<Response> Responses { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserClassCourse> UserClassCourses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ClassMap());
            modelBuilder.Configurations.Add(new ClassTopicMap());
            modelBuilder.Configurations.Add(new CourseMap());
            modelBuilder.Configurations.Add(new CourseTopicMap());
            modelBuilder.Configurations.Add(new KWLentryMap());
            modelBuilder.Configurations.Add(new ResponseMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
            modelBuilder.Configurations.Add(new TopicMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new UserClassCourseMap());
        }
    }
}
