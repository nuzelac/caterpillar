using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Caterpillar.Models.Mapping
{
	public class ClassMap : EntityTypeConfiguration<Class>
	{
		public ClassMap()
		{
			// Primary Key
			this.HasKey(t => t.Id);

			// Properties
			this.Property(t => t.Name)
				.IsRequired()
                .HasMaxLength(200);

			// Table & Column Mappings
			this.ToTable("Class");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.Name).HasColumnName("Name");
			this.Property(t => t.Year).HasColumnName("Year");
		}
	}
}
