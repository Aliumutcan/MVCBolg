using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcBlog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
             name: "MakaleDuzenle",
             url: "Makale/Duzenle/{id}/Makale-yeniden-hazirlaniyor",
             defaults: new { controller = "Yonetim", action = "YonetimMakaleYaz", id = "" }
         );
            routes.MapRoute(
              name: "Makaleler",
              url: "Makale/islemleri/gerceklestiriliyor/{islem}/{id}",
              defaults: new { controller = "Yonetim", action = "YonetimMakaleler", id = "", islem = "yok" }
          );
            //yukarısı yönetim paneli routeleri
            routes.MapRoute(
                name: "MakaleDetay",
                url: "Makale/Detay/{id}/{baslik}.html",
                defaults: new { controller = "Makale", action = "MakaleDetay", id = "",baslik="" }
        );
            routes.MapRoute(
                name: "MakaleDetayBR",
                url: "Makale/Detay/{yorumid}/{islem}/onaylandi/{id}.html",
                defaults: new { controller = "Makale", action = "MakaleYorumBR", id = "", islem = "",yorumid="" }
        );
            routes.MapRoute(
               name: "MakaleGetir",
               url: "Makaleler/{sayfa}.html",
               defaults: new { controller = "Makale", action = "MakalelerGetir", sayfa = "1" }
       );
            routes.MapRoute(
                name: "MakaleKategoriGetir",
                url: "Makaleler-kategori/{kategoriid}/{kategori}/{sayfa}.html",
                defaults: new { controller = "Makale", action = "MakalelerGetir", kategori = "", kategoriid = "", sayfa = "1" }
        );
            routes.MapRoute(
                name: "MakaleEtiketGetir",
                url: "Makaleler-etiket/{etiketid}/{etiket}/{sayfa}.html",
                defaults: new { controller = "Makale", action = "MakalelerGetir", etiket = "", etiketid = "", sayfa = "1" }
        );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{defaultid}",
                defaults: new { controller = "Makale", action = "MakalelerGetir", defaultid = "" }
        );



        }
    }
}