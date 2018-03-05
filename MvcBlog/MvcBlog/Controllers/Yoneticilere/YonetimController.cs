using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBlog.Models;
using System.Drawing;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Web.Helpers;
using MvcBlog.Controllers.Yoneticilere;
using System.Web.Security;
using Elmah;



namespace MvcBlog.Controllers.Yoneticilere
{
    [Authorize]
    public class YonetimController : Controller
    {
        //
        // GET: /Yonetim/
        public static BlogDBContext context = new BlogDBContext();
        public static string display = "none";
        [AllowAnonymous]
        public ActionResult YonetimGiris(string mail = "", string sifre = "")
        {
            if (mail != "" && sifre != "")
            {
                string yenisifre = GenelController.MD5eDonustur(sifre);
                List<Yazarlar> sorgu = context.Yazarlars.Where(x => x.yazarmail == mail).Where(z => z.yazarsifre == yenisifre).ToList();
                //Yazarlar yazar = from y in context.Yazarlars
                //                 where y.yazarmail == mail && y.yazarsifre == sifre
                //                 select y;
                if (sorgu.Count() == 1)
                {
                    FormsAuthentication.SetAuthCookie("yazar", false);
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(sorgu[0].yazarid.ToString(), false, 30);
                    string sifrelicookie = FormsAuthentication.Encrypt(ticket);
                    HttpCookie cookies = new HttpCookie(FormsAuthentication.FormsCookieName, sifrelicookie);
                    cookies.HttpOnly = true;
                    Response.Cookies.Add(cookies);
                    return RedirectToAction("YonetimAnasayfa", "Yonetim");
                }

                else if (sorgu.Count() > 1)
                {
                    return View();
                }
                else
                    return View();

            }
            else
                return View();
        }

        public ActionResult YonetimCikis(string defaultid)
        {
            if (defaultid == "cikis")
            {
                FormsAuthentication.SignOut();
                display = "none";
                return RedirectToAction("YonetimGiris");
            }
            else
                return RedirectToAction("YonetimAnasayfa");
        }

        public ActionResult YonetimAnasayfa(string id = "")
        {
            display = "block";
            if (id != "" && id == "Cikis")
            {
                FormsAuthentication.SignOut();
                return View("YonetimGiris");
            }
            else
            {
                return View();
            }

        }

        #region Makaleler
        public ActionResult YonetimMakaleler(string islem, string id)
        {
            if (islem == "sil")
                return RedirectToAction("YonetimMakaleSil", new { id = id, islem = islem });
            else if (islem == "acik" || islem == "kapali")
                return RedirectToAction("YonetimMakaleAcKapa", new { id = id, islem = islem });

            var makaleler = context.Makalelers.ToList();
            return View(makaleler);

        }

        public ActionResult YonetimMakaleAcKapa(string islem, string id)
        {
            if ((islem != null || islem != "") && (id != null || id != ""))
            {
                int yeniid = 0;
                if (islem == "acik" && id != null && id != "" && int.TryParse(id, out yeniid))
                {
                    Makaleler durum = context.Makalelers.Where(x => x.makaleid == yeniid).Take(1).FirstOrDefault();
                    durum.makaledurum = false;
                    context.SaveChanges();
                    islem = null; id = null;
                }
                else if (islem == "kapali" && id != null && id != "" && int.TryParse(id, out yeniid))
                {
                    Makaleler durum = context.Makalelers.Where(x => x.makaleid == yeniid).Take(1).FirstOrDefault();
                    durum.makaledurum = true;
                    context.SaveChanges();
                    islem = null; id = null;
                }
            }
            return RedirectToAction("YonetimMakaleler");
        }

        public ActionResult YonetimMakaleSil(string islem, string id)
        {
            int yeniid = 0;

            if (islem == "sil" && id != null && id != "" && int.TryParse(id, out yeniid))
            {
                var silinecek = context.Makalelers.Where(x => x.makaleid == yeniid).FirstOrDefault();
                context.Database.ExecuteSqlCommand("delete from Yorumlar where yorummakaleID={0}", silinecek.makaleid);
                context.Galeris.Remove(silinecek.Galeri);
                context.Makalelers.Remove(silinecek);
                context.SaveChanges();
                islem = null; id = null;
            }

            return RedirectToAction("YonetimMakaleler");
        }
        #endregion

        #region Makale yaz/duzenle
        [ValidateInput(false)]
        public ActionResult YonetimMakaleYaz(Makaleler makale, string etiketler, HttpPostedFileBase resim, string id)
        {
            using (GenelController kontrol = new GenelController())
            {
                int yeniid = 0;
                if (makale.makalebaslik == null && etiketler == null && int.TryParse(id, out yeniid))
                {

                    ViewBag.kategoriler = context.Kategorilers.ToList();
                    return View(context.Makalelers.Where(x => x.makaleid == yeniid).Take(1).FirstOrDefault());
                }
                else if (makale.makalebaslik != null && etiketler != null && int.TryParse(id, out yeniid))
                {
                    Makaleler makale2 = context.Makalelers.Where(x => x.makaleid == yeniid).Take(1).FirstOrDefault();
                    makale2.makaleyazarID = yazarid();
                    makale2.makaleyayintarihi = DateTime.Now;
                    if (resim != null)
                        makale2.makalekapakresimID = kontrol.ResimKaydet(resim, yazarid());
                    makale2.makaledurum = true;
                    makale2.makalekategoriID = makale.makalekategoriID;
                    makale2.makaleicerik = makale.makaleicerik;
                    makale2.makalebaslik = makale.makalebaslik;
                    kontrol.Etiketlerikaydet(etiketler, makale2);
                    context.SaveChanges();
                    return RedirectToAction("YonetimMakaleler");
                }
                else if (makale.makalebaslik != null && makale.makaleicerik != null && resim != null && makale.makalekategoriID > 0 && int.TryParse(id, out yeniid) == false)
                {

                    makale.makalekapakresimID = kontrol.ResimKaydet(resim, yazarid());
                    makale.makaleyazarID = yazarid();
                    makale.makaleyayintarihi = DateTime.Now;
                    makale.makaledurum = true;
                    context.Makalelers.Add(makale);
                    context.SaveChanges();


                    string[] etiket = etiketler.Split(',');
                    foreach (string item in etiket)
                    {
                        Etitekler gelenetiket = context.Etiteklers.FirstOrDefault(x => x.etiketadi.ToLower() == item.Trim().ToLower());
                        if (gelenetiket == null)
                        {
                            gelenetiket = new Etitekler();
                            gelenetiket.etiketadi = item;
                            makale.Etiteklers.Add(gelenetiket);
                            context.SaveChanges();
                        }
                    }

                }
            }



            Makaleler m = new Makaleler();
            ViewBag.kategoriler = context.Kategorilers.ToList();
            return View(m);




        }

        [ValidateInput(false)]
        public ActionResult YonetimMakaleDüzenle(Makaleler makale, string etiketler, HttpPostedFileBase resim, int id)
        {
            using (GenelController kontrol = new GenelController())
            {
                if (makale != null && etiketler != null && makale.makaleicerik != null && makale.makalekategoriID > 0 && id != null)
                {
                    Makaleler gelenmakale = context.Makalelers.Where(x => x.makaleid == id).Take(1).FirstOrDefault();
                    if (resim != null)
                        gelenmakale.makalekapakresimID = kontrol.ResimKaydet(resim, yazarid());
                    gelenmakale.makaleyayintarihi = DateTime.Now;
                    gelenmakale.makaleyazarID = yazarid();
                    kontrol.Etiketlerikaydet(etiketler, gelenmakale);
                    gelenmakale.makalebaslik = makale.makalebaslik;
                    gelenmakale.makaledurum = makale.makaledurum;
                    gelenmakale.makaleicerik = makale.makaleicerik;
                    gelenmakale.makalekategoriID = makale.makalekategoriID;
                    context.SaveChanges();


                    return RedirectToAction("YonetimMakaleler","Yonetim");
                }
                else
                    return View(context.Makalelers.Where(x => x.makaleid == id).Take(1));
            }

        }
        #endregion

        #region yorumlar

        public ActionResult YonetimYorumlar()
        {
            return View(context.Yorumlars.ToList());
        }

        public ActionResult YonetimYorumSil(string id)
        {
            int yeniid = 0;
            if (int.TryParse(id, out yeniid))
            {
                Yorumlar silinecek = (Yorumlar)context.Yorumlars.Where(x => x.yorumid == yeniid).Take(1).FirstOrDefault();
                context.Yorumlars.Remove(silinecek);
                context.SaveChanges();
                id = null;
            }
            return RedirectToAction("YonetimYorumlar");
        }

        public ActionResult YonetimYorumDurum(string defaultid)
        {
            int yeniid = 0;
            if (int.TryParse(defaultid, out yeniid))
            {
                Yorumlar yorum = context.Yorumlars.Where(x => x.yorumid == yeniid).Take(1).FirstOrDefault();
                if (yorum.yorumdurum == true)
                    yorum.yorumdurum = false;
                else
                {
                    yorum.yorumdurum = true;
                    context.SaveChanges();
                    List<Yorumlar> yorumlar = context.Yorumlars.Where(x => x.yorummakaleID == yorum.yorummakaleID).ToList();
                    GenelController.MakaleYorumHaberVer(yorumlar, yorum.yorumid);
                }

            }
            return RedirectToAction("YonetimYorumlar");
        }

        #endregion

        #region kategoriislemleri

        public ActionResult Kategoriler()
        {
            return View(context.Kategorilers.ToList());
        }

        public ActionResult KategoriSil(string defaultid)
        {
            int yeniid = 0;
            if (int.TryParse(defaultid, out yeniid))
            {
                MvcBlog.Models.Kategoriler k = context.Kategorilers.Where(x => x.kategoriid == yeniid).Take(1).FirstOrDefault();
                context.Kategorilers.Remove(k);
                context.SaveChanges();
            }
            return RedirectToAction("Kategoriler");
        }

        [HttpPost]
        public ActionResult KategoriDuzenleEkle(string kategoriadi1, string kategoriid1, string kategoriadi2, string kategoriadi)
        {
            int yaniid = 0;
            if (int.TryParse(kategoriid1, out yaniid) && kategoriadi1 != null && kategoriadi2 != null)
            {
                IEnumerable<MvcBlog.Models.Kategoriler> gelen = context.Kategorilers.Where(x => x.kategoriadi.Trim().ToLower() == kategoriadi2.Trim().ToLower());
                if (gelen.Count() > 0)
                    return RedirectToAction("Kategoriler");

                MvcBlog.Models.Kategoriler k = context.Kategorilers.Where(x => x.kategoriid == yaniid).Take(1).FirstOrDefault();
                k.kategoriadi = kategoriadi2;
                context.SaveChanges();
            }
            else if (kategoriadi != null || kategoriadi != "")
            {
                IEnumerable<MvcBlog.Models.Kategoriler> gelen = context.Kategorilers.Where(x => x.kategoriadi.Trim().ToLower() == kategoriadi.Trim().ToLower());
                if (gelen.Count() != 0)
                    return RedirectToAction("Kategoriler");

                MvcBlog.Models.Kategoriler k = new MvcBlog.Models.Kategoriler();
                k.kategoriadi = kategoriadi;
                context.Kategorilers.Add(k);
                context.SaveChanges();
            }
            return RedirectToAction("Kategoriler");
        }
        #endregion

        #region etiketler
        public ActionResult YonetimEtiketler()
        {
            return View(context.Etiteklers.ToList());
        }

        public ActionResult YonetimEtiketSil(string defaultid)
        {
            int yeniid = 0;
            if (int.TryParse(defaultid, out yeniid))
            {
                context.Database.ExecuteSqlCommand("delete from MakaleEtiket where etiketid={0}", yeniid);
                context.Database.ExecuteSqlCommand("delete from Etitekler where etiketid={0}", yeniid);
                context.SaveChanges();
            }

            return RedirectToAction("YonetimEtiketler");
        }
        #endregion

        #region uyeler

        public ActionResult YonetimUyelerGetir()
        {
            IEnumerable<Uyeler> uyeler = context.Uyelers.OrderByDescending(x=>x.uyeid).ToList();
            return View(uyeler);
        }

        public ActionResult YonetimUyelerSil(string defaultid)
        {
            int yeniid;
            if (int.TryParse(defaultid, out yeniid))
            {
                Uyeler uyeler = context.Uyelers.Where(x => x.uyeid == yeniid).Take(1).FirstOrDefault();
                context.Uyelers.Remove(uyeler);
                context.SaveChanges();
            }
            return RedirectToAction("YonetimUyelerGetir");
        }

        #endregion

        public int yazarid()
        {
            HttpCookie cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                string id = ticket.Name;
                int yeniid;
                int.TryParse(id, out yeniid);
                return yeniid;
            }
            else
                RedirectToAction("YonetimGiris");

            return 0;
        }
    }
}
