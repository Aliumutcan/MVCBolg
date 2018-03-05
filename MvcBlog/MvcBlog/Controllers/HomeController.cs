using MvcBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using MvcBlog.Controllers.Yoneticilere;
using System.Web.Security;

namespace MvcBlog.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        BlogDBContext context = new BlogDBContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult yanpanelkategoriler()
        {
            return View(context.Kategorilers.ToList());
        }

        public ActionResult ustpanelkategoriler()
        {
            IEnumerable<Kategoriler> k = context.Kategorilers.ToList();
            return View(k);
        }

        public ActionResult enpolulerler()
        {
            IEnumerable<Makaleler> m = context.Makalelers.OrderByDescending(x => x.makalebegeni).Where(x => x.makaledurum == true).Take(6).ToList();
            return View(m);
        }

        public ActionResult yanpaneletiketler()
        {
            ViewBag.etiketler = context.Etiteklers.ToList();
            return View();
        }

        public ActionResult Iletisim()
        {
            return View();
        }

        #region uye islemleri
        [HttpGet]
        public ActionResult UyeGiris()
        {
            if (Session["uye"] != null)
            {
                List<Uyeler> uye = (List<Uyeler>)Session["uye"];
                TempData["uye"] = uye[0].uyeadi + " " + uye[0].uyesoyadi;
            }else
                TempData["uye"] = null;

            return View();
        }

        [HttpPost]
        public ActionResult UyeGiris(string uyemail,string uyesifre)
        {
            if (uyemail != null && uyesifre != null)
            {
                string yenisifre = GenelController.MD5eDonustur(uyesifre);
                List<Uyeler> sorgu = context.Uyelers.Where(x => x.uyemail == uyemail).Where(z => z.uyesifre == yenisifre).ToList();

                if (sorgu.Count() == 1)
                {
                    Session["uye"] = sorgu;
                    TempData["uye"] = sorgu[0].uyeid + " " + sorgu[0].uyesoyadi;
                }
                return RedirectToAction("MakalelerGetir", "Makale");
            }
            else
                return View();
            
            
        }

        public ActionResult UyeCikis()
        {
            if (Session["uye"] != null)
            {
                Session["uye"] = null;
                TempData["uye"] = null;
            }
            return RedirectToAction("MakalelerGetir", "Makale");
        }

        public ActionResult UyeKayitOl(Uyeler uyeler, string uyesifretekrar)
        {
            if (uyeler.uyeadi != null && uyeler.uyemail != null && uyeler.uyesifre != null && uyeler.uyesoyadi != null && uyesifretekrar==uyeler.uyesifre)
            {
                uyeler.uyesifre = GenelController.MD5eDonustur(uyeler.uyesifre);
                uyeler.uyekayittarihi = DateTime.Now;
                context.Uyelers.Add(uyeler);
                context.SaveChanges();
                return RedirectToAction("MakalelerGetir", "Makale");
            }
            else
            {
                return View();
            }
            
        }
        #endregion

        public ActionResult Error()
        {
            return View();
        }
    }
}
