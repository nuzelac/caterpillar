using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Caterpillar.Models.Mapping
{
    public class IsResponseOnMap : EntityTypeConfiguration<IsResponseOn>
    {
        public IsResponseOnMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("IsResponseOn");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.EntryId).HasColumnName("EntryId");
            this.Property(t => t.ResponseId).HasColumnName("ResponseId");

            // Relationships
            this.HasRequired(t => t.KWLentry)
                .WithMany(t => t.IsResponseOns)
                .HasForeignKey(d => d.EntryId);
            this.HasRequired(t => t.KWLentry1)
                .WithMany(t => t.IsResponseOns1)
                .HasForeignKey(d => d.ResponseId);

        }
    }
}
