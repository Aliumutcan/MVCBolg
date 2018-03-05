using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MvcBlog.Models.Mapping
{
    public class UyelerMap : EntityTypeConfiguration<Uyeler>
    {
        public UyelerMap()
        {
            // Primary Key
            this.HasKey(t => t.uyeid);

            // Properties
            this.Property(t => t.uyeadi)
                .HasMaxLength(50);

            this.Property(t => t.uyesoyadi)
                .HasMaxLength(50);

            this.Property(t => t.uyemail)
                .HasMaxLength(50);

            this.Property(t => t.uyesifre)
                .HasMaxLength(50);

            this.Property(t => t.uyetanima)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("Uyeler");
            this.Property(t => t.uyeid).HasColumnName("uyeid");
            this.Property(t => t.uyeadi).HasColumnName("uyeadi");
            this.Property(t => t.uyesoyadi).HasColumnName("uyesoyadi");
            this.Property(t => t.uyemail).HasColumnName("uyemail");
            this.Property(t => t.uyesifre).HasColumnName("uyesifre");
            this.Property(t => t.uyetanima).HasColumnName("uyetanima");
            this.Property(t => t.uyekayittarihi).HasColumnName("uyekayittarihi");
        }
    }
}
