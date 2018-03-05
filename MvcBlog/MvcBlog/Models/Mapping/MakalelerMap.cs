using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MvcBlog.Models.Mapping
{
    public class MakalelerMap : EntityTypeConfiguration<Makaleler>
    {
        public MakalelerMap()
        {
            // Primary Key
            this.HasKey(t => t.makaleid);

            // Properties
            this.Property(t => t.makalebaslik)
                .HasMaxLength(200);

            this.Property(t => t.makalehostname)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Makaleler");
            this.Property(t => t.makaleid).HasColumnName("makaleid");
            this.Property(t => t.makalebaslik).HasColumnName("makalebaslik");
            this.Property(t => t.makalekapakresimID).HasColumnName("makalekapakresimID");
            this.Property(t => t.makaleicerik).HasColumnName("makaleicerik");
            this.Property(t => t.makaleyazarID).HasColumnName("makaleyazarID");
            this.Property(t => t.makalekategoriID).HasColumnName("makalekategoriID");
            this.Property(t => t.makaleyayintarihi).HasColumnName("makaleyayintarihi");
            this.Property(t => t.makalebegeni).HasColumnName("makalebegeni");
            this.Property(t => t.makalebegenenkisisayisi).HasColumnName("makalebegenenkisisayisi");
            this.Property(t => t.makaledurum).HasColumnName("makaledurum");
            this.Property(t => t.makalehostname).HasColumnName("makalehostname");
            this.Property(t => t.gorunum).HasColumnName("gorunum");

            // Relationships
            this.HasOptional(t => t.Galeri)
                .WithMany(t => t.Makalelers)
                .HasForeignKey(d => d.makalekapakresimID);
            this.HasOptional(t => t.Kategoriler)
                .WithMany(t => t.Makalelers)
                .HasForeignKey(d => d.makalekategoriID);
            this.HasOptional(t => t.Yazarlar)
                .WithMany(t => t.Makalelers)
                .HasForeignKey(d => d.makaleyazarID);

        }
    }
}
