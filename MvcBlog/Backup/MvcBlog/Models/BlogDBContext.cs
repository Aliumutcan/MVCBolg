using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using MvcBlog.Models.Mapping;

namespace MvcBlog.Models
{
    public partial class BlogDBContext : DbContext
    {
        static BlogDBContext()
        {
            Database.SetInitializer<BlogDBContext>(null);
        }

        public BlogDBContext()
            : base("Name=BlogDBContext")
        {
        }

        public DbSet<ELMAH_Error> ELMAH_Error { get; set; }
        public DbSet<Etitekler> Etiteklers { get; set; }
        public DbSet<Galeri> Galeris { get; set; }
        public DbSet<Kategoriler> Kategorilers { get; set; }
        public DbSet<Makaleler> Makalelers { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<Uyeler> Uyelers { get; set; }
        public DbSet<Yazarlar> Yazarlars { get; set; }
        public DbSet<Yorumlar> Yorumlars { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ELMAH_ErrorMap());
            modelBuilder.Configurations.Add(new EtiteklerMap());
            modelBuilder.Configurations.Add(new GaleriMap());
            modelBuilder.Configurations.Add(new KategorilerMap());
            modelBuilder.Configurations.Add(new MakalelerMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
            modelBuilder.Configurations.Add(new UyelerMap());
            modelBuilder.Configurations.Add(new YazarlarMap());
            modelBuilder.Configurations.Add(new YorumlarMap());
        }
    }
}
