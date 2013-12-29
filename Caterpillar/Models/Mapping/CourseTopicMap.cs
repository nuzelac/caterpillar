using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Caterpillar.Models.Mapping
{
	public class CourseTopicMap : EntityTypeConfiguration<CourseTopic>
	{
		public CourseTopicMap()
		{
			// Primary Key
			this.HasKey(t => t.Id);

			// Properties
			// Table & Column Mappings
			this.ToTable("CourseTopic");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.CourseId).HasColumnName("CourseId");
			this.Property(t => t.TopicId).HasColumnName("TopicId");

			// Relationships
			this.HasRequired(t => t.Course)
				.WithMany(t => t.CourseTopics)
				.HasForeignKey(d => d.CourseId);
			this.HasRequired(t => t.Topic)
				.WithMany(t => t.CourseTopics)
				.HasForeignKey(d => d.TopicId);

		}
	}
}
