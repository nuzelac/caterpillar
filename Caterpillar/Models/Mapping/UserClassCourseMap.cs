using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Caterpillar.Models.Mapping
{
    public class UserClassCourseMap : EntityTypeConfiguration<UserClassCourse>
    {
        public UserClassCourseMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("UserClassCourse");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.ClassId).HasColumnName("ClassId");
            this.Property(t => t.CourseId).HasColumnName("CourseId");

            // Relationships
            this.HasRequired(t => t.Class)
                .WithMany(t => t.UserClassCourses)
                .HasForeignKey(d => d.ClassId);
            this.HasRequired(t => t.Course)
                .WithMany(t => t.UserClassCourses)
                .HasForeignKey(d => d.CourseId);
            this.HasRequired(t => t.User)
                .WithMany(t => t.UserClassCourses)
                .HasForeignKey(d => d.UserId);

        }
    }
}
