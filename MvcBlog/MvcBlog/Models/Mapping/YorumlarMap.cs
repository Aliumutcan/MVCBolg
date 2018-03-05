using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MvcBlog.Models.Mapping
{
    public class YorumlarMap : EntityTypeConfiguration<Yorumlar>
    {
        public YorumlarMap()
        {
            // Primary Key
            this.HasKey(t => t.yorumid);

            // Properties
            this.Property(t => t.yorumyazari)
                .HasMaxLength(200);

            this.Property(t => t.yorummail)
                .HasMaxLength(150);

            this.Property(t => t.yorumicerik)
                .HasMaxLength(500);

            this.Property(t => t.yorumip)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Yorumlar");
            this.Property(t => t.yorumid).HasColumnName("yorumid");
            this.Property(t => t.yorumyazari).HasColumnName("yorumyazari");
            this.Property(t => t.yorummail).HasColumnName("yorummail");
            this.Property(t => t.yorumicerik).HasColumnName("yorumicerik");
            this.Property(t => t.yorumtarihi).HasColumnName("yorumtarihi");
            this.Property(t => t.yorummakaleID).HasColumnName("yorummakaleID");
            this.Property(t => t.yorumhaberdarolmakistiyorum).HasColumnName("yorumhaberdarolmakistiyorum");
            this.Property(t => t.yorumbegeni).HasColumnName("yorumbegeni");
            this.Property(t => t.yorumred).HasColumnName("yorumred");
            this.Property(t => t.yorumip).HasColumnName("yorumip");
            this.Property(t => t.yorumdurum).HasColumnName("yorumdurum");

            // Relationships
            this.HasOptional(t => t.Makaleler)
                .WithMany(t => t.Yorumlars)
                .HasForeignKey(d => d.yorummakaleID);

        }
    }
}
