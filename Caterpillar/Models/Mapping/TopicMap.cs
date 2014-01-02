using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Caterpillar.Models.Mapping
{
	public class TopicMap : EntityTypeConfiguration<Topic>
	{
		public TopicMap()
		{
			// Primary Key
			this.HasKey(t => t.Id);

			// Properties
			this.Property(t => t.Name)
				.IsRequired();

			// Table & Column Mappings
			this.ToTable("Topic");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.Name).HasColumnName("Name");
			this.Property(t => t.StartDate).HasColumnName("StartDate");
			this.Property(t => t.EndDate).HasColumnName("EndDate");
			this.Property(t => t.CreatedByUserId).HasColumnName("CreatedByUserId");
			this.Property(t => t.LastEditedByUserId).HasColumnName("LastEditedByUserId");
			this.Property(t => t.LastEditDate).HasColumnName("LastEditDate");
		}
	}
}
