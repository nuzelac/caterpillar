using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Caterpillar.Models.Mapping
{
	public class ResponseMap : EntityTypeConfiguration<Response>
	{
		public ResponseMap()
		{
			// Primary Key
			this.HasKey(t => t.Id);

			// Properties
			// Table & Column Mappings
			this.ToTable("Response");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.EntryId).HasColumnName("EntryId");
			this.Property(t => t.UserId).HasColumnName("UserId");
			this.Property(t => t.Response1).HasColumnName("Response");
			this.Property(t => t.Correction).HasColumnName("Correction");
			this.Property(t => t.Points).HasColumnName("Points");

			// Relationships
			this.HasRequired(t => t.KWLentry)
				.WithMany(t => t.Responses)
				.HasForeignKey(d => d.EntryId);
			this.HasRequired(t => t.User)
				.WithMany(t => t.Responses)
				.HasForeignKey(d => d.UserId);

		}
	}
}
