using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Caterpillar.Models.Mapping
{
	public class ClassTopicMap : EntityTypeConfiguration<ClassTopic>
	{
		public ClassTopicMap()
		{
			// Primary Key
			this.HasKey(t => t.Id);

			// Properties
			// Table & Column Mappings
			this.ToTable("ClassTopic");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.ClassId).HasColumnName("ClassId");
			this.Property(t => t.TopicId).HasColumnName("TopicId");

			// Relationships
			this.HasRequired(t => t.Class)
				.WithMany(t => t.ClassTopics)
				.HasForeignKey(d => d.ClassId);
			this.HasRequired(t => t.Topic)
				.WithMany(t => t.ClassTopics)
				.HasForeignKey(d => d.TopicId);

		}
	}
}
