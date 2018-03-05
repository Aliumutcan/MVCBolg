using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MvcBlog.Models.Mapping
{
    public class EtiteklerMap : EntityTypeConfiguration<Etitekler>
    {
        public EtiteklerMap()
        {
            // Primary Key
            this.HasKey(t => t.etiketid);

            // Properties
            this.Property(t => t.etiketadi)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Etitekler");
            this.Property(t => t.etiketid).HasColumnName("etiketid");
            this.Property(t => t.etiketadi).HasColumnName("etiketadi");

            // Relationships
            this.HasMany(t => t.Makalelers)
                .WithMany(t => t.Etiteklers)
                .Map(m =>
                    {
                        m.ToTable("MakaleEtiket");
                        m.MapLeftKey("etiketid");
                        m.MapRightKey("makaleid");
                    });


        }
    }
}
