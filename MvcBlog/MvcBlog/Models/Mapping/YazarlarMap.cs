using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MvcBlog.Models.Mapping
{
    public class YazarlarMap : EntityTypeConfiguration<Yazarlar>
    {
        public YazarlarMap()
        {
            // Primary Key
            this.HasKey(t => t.yazarid);

            // Properties
            this.Property(t => t.yazaradi)
                .HasMaxLength(100);

            this.Property(t => t.yazarsoyadi)
                .HasMaxLength(100);

            this.Property(t => t.yazarmail)
                .HasMaxLength(100);

            this.Property(t => t.yazarsifre)
                .HasMaxLength(200);

            this.Property(t => t.yazartel)
                .HasMaxLength(15);

            this.Property(t => t.yazarhakkinda)
                .HasMaxLength(200);

            this.Property(t => t.yazaradres)
                .HasMaxLength(300);

            this.Property(t => t.yazarTC)
                .HasMaxLength(11);

            // Table & Column Mappings
            this.ToTable("Yazarlar");
            this.Property(t => t.yazarid).HasColumnName("yazarid");
            this.Property(t => t.yazaradi).HasColumnName("yazaradi");
            this.Property(t => t.yazarsoyadi).HasColumnName("yazarsoyadi");
            this.Property(t => t.yazarmail).HasColumnName("yazarmail");
            this.Property(t => t.yazarsifre).HasColumnName("yazarsifre");
            this.Property(t => t.yazartel).HasColumnName("yazartel");
            this.Property(t => t.yazarresimID).HasColumnName("yazarresimID");
            this.Property(t => t.yazarhakkinda).HasColumnName("yazarhakkinda");
            this.Property(t => t.yazaradres).HasColumnName("yazaradres");
            this.Property(t => t.yazarTC).HasColumnName("yazarTC");
            this.Property(t => t.yazartarihi).HasColumnName("yazartarihi");
        }
    }
}
