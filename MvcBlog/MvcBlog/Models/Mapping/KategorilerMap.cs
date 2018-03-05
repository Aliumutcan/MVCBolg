using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MvcBlog.Models.Mapping
{
    public class KategorilerMap : EntityTypeConfiguration<Kategoriler>
    {
        public KategorilerMap()
        {
            // Primary Key
            this.HasKey(t => t.kategoriid);

            // Properties
            this.Property(t => t.kategoriadi)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Kategoriler");
            this.Property(t => t.kategoriid).HasColumnName("kategoriid");
            this.Property(t => t.kategoriadi).HasColumnName("kategoriadi");
        }
    }
}
