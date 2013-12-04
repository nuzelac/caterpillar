using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Caterpillar.Models.Mapping
{
    public class KWLentryMap : EntityTypeConfiguration<KWLentry>
    {
        public KWLentryMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("KWLentry");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Entry).HasColumnName("Entry");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.KWLentries)
                .HasForeignKey(d => d.UserId);

        }
    }
}
