using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MvcBlog.Models.Mapping
{
    public class ELMAH_ErrorMap : EntityTypeConfiguration<ELMAH_Error>
    {
        public ELMAH_ErrorMap()
        {
            // Primary Key
            this.HasKey(t => t.ErrorId);

            // Properties
            this.Property(t => t.Application)
                .IsRequired()
                .HasMaxLength(60);

            this.Property(t => t.Host)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Type)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Source)
                .IsRequired()
                .HasMaxLength(60);

            this.Property(t => t.Message)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.User)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Sequence)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.AllXml)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ELMAH_Error");
            this.Property(t => t.ErrorId).HasColumnName("ErrorId");
            this.Property(t => t.Application).HasColumnName("Application");
            this.Property(t => t.Host).HasColumnName("Host");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Source).HasColumnName("Source");
            this.Property(t => t.Message).HasColumnName("Message");
            this.Property(t => t.User).HasColumnName("User");
            this.Property(t => t.StatusCode).HasColumnName("StatusCode");
            this.Property(t => t.TimeUtc).HasColumnName("TimeUtc");
            this.Property(t => t.Sequence).HasColumnName("Sequence");
            this.Property(t => t.AllXml).HasColumnName("AllXml");
        }
    }
}
