using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MvcBlog.Models.Mapping
{
    public class GaleriMap : EntityTypeConfiguration<Galeri>
    {
        public GaleriMap()
        {
            // Primary Key
            this.HasKey(t => t.galeriid);

            // Properties
            this.Property(t => t.adi)
                .HasMaxLength(50);

            this.Property(t => t.kucukresimyol)
                .HasMaxLength(500);

            this.Property(t => t.ortaresimyol)
                .HasMaxLength(500);

            this.Property(t => t.buyukresimyol)
                .HasMaxLength(500);

            this.Property(t => t.videoyol)
                .HasMaxLength(200);

            this.Property(t => t.resimtipi)
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("Galeri");
            this.Property(t => t.galeriid).HasColumnName("galeriid");
            this.Property(t => t.adi).HasColumnName("adi");
            this.Property(t => t.kucukresimyol).HasColumnName("kucukresimyol");
            this.Property(t => t.ortaresimyol).HasColumnName("ortaresimyol");
            this.Property(t => t.buyukresimyol).HasColumnName("buyukresimyol");
            this.Property(t => t.videoyol).HasColumnName("videoyol");
            this.Property(t => t.ekleyenID).HasColumnName("ekleyenID");
            this.Property(t => t.eklemetarihi).HasColumnName("eklemetarihi");
            this.Property(t => t.goruntulenme).HasColumnName("goruntulenme");
            this.Property(t => t.begeni).HasColumnName("begeni");
            this.Property(t => t.kucukresimimage).HasColumnName("kucukresimimage");
            this.Property(t => t.ortaresimimage).HasColumnName("ortaresimimage");
            this.Property(t => t.buyukresimimage).HasColumnName("buyukresimimage");
            this.Property(t => t.resimtipi).HasColumnName("resimtipi");

            // Relationships
            this.HasOptional(t => t.Yazarlar)
                .WithMany(t => t.Galeris)
                .HasForeignKey(d => d.ekleyenID);

        }
    }
}
