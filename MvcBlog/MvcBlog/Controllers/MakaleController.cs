using MvcBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.Web.Helpers;
using MvcBlog.Controllers.Yoneticilere;

namespace MvcBlog.Controllers
{
    public class MakaleController : Controller
    {
        //
        // GET: /Makale/
        BlogDBContext context = new BlogDBContext();
        public ActionResult MakalelerGetir(string sayfa = "1", string kategoriid = "", string etiketid = "", string aranan = "")
        {
            int yenisayfa = 1, yeniid;
            if (int.TryParse(kategoriid, out yeniid) && int.TryParse(sayfa, out yenisayfa))
            {
                IEnumerable<Makaleler> makeleler = context.Makalelers.Where(x => x.Kategoriler.kategoriid == yeniid).Where(x => x.makaledurum == true).ToList().ToPagedList(yenisayfa, 1);
                return View(makeleler);
            }
            else if (int.TryParse(etiketid, out yeniid) && int.TryParse(sayfa, out yenisayfa))
            {
                IEnumerable<Etitekler> etiketler = context.Etiteklers.Where(x => x.etiketid == yeniid).ToList();
                //IEnumerable<Makaleler> makeleler = context.Makalelers.Where().ToList().ToPagedList(yenisayfa, 1);
                IEnumerable<Makaleler> makeleler = etiketler.Select(x => x.Makalelers).Single();
                return View(makeleler.Where(x => x.makaledurum == true).ToPagedList(yenisayfa, 1));
            }
            else if (aranan.Count() >= 3)
            {
                IEnumerable<Makaleler> makaleler = context.Makalelers.Where(x => x.makalebaslik.IndexOf(aranan) >= 0).Where(x => x.makaledurum == true).ToList().ToPagedList(yenisayfa, 1);
                return View(makaleler);
            }
            else if (int.TryParse(sayfa, out yenisayfa))
            {
                IEnumerable<Makaleler> makeleler = context.Makalelers.Where(x => x.makaledurum == true).ToList().ToPagedList(yenisayfa, 1);
                return View(makeleler);
            }
            else
                return RedirectToAction("MakalelerGetir");
        }

        public ActionResult MakaleDetay(string id)
        {
            int yeniid = 0;
            if (int.TryParse(id, out yeniid))
            {
                HttpCookie cookie=Request.Cookies[Request.UserHostAddress];
                Makaleler makale=null;
                if (cookie==null)
                {
                    cookie = new HttpCookie(Request.UserHostAddress);
                    makale = context.Makalelers.Where(x => x.makaleid == yeniid).Take(1).FirstOrDefault();
                    makale.gorunum++;
                    context.SaveChanges();
                    cookie.Values.Add("id", id);
                    cookie.Expires.Add(TimeSpan.FromDays(1));
                    Response.Cookies.Add(cookie);
                }
                else
                {
                    int sayac = 0;
                    foreach (var item in cookie.Values.GetValues("id"))
                    {
                        if (item == id)
                            sayac++;
                    }
                    if (sayac==0)
                    {
                        makale = context.Makalelers.Where(x => x.makaleid == yeniid).Take(1).FirstOrDefault();
                        makale.gorunum++;
                        context.SaveChanges();
                        cookie.Values.Add("id", id);
                        cookie.Expires.Add(TimeSpan.FromDays(1));
                        Response.Cookies.Add(cookie);
                    }else
                        makale = context.Makalelers.Where(x => x.makaleid == yeniid).Take(1).FirstOrDefault();
                }

                
                return View(makale);
            }
            else
                return RedirectToAction("MakalelerGetir");
        }

        public ActionResult BenzerMakaleler()
        {
            IEnumerable<Makaleler> m = context.Makalelers.OrderBy(x => Guid.NewGuid()).Where(x => x.makaledurum == true).Take(3).ToList();
            return View(m);
        }

        #region yorum ile ilgili kısımlar
        public ActionResult MakaleYorumlari(string id, string yorumid)
        {
            int yeniid = 0;
            if (int.TryParse(id, out yeniid))
            {
                IEnumerable<Yorumlar> y = context.Yorumlars.Where(x => x.Makaleler.makaleid == yeniid);
                ViewData["makaleid"] = id;
                return View(y);
            }
            else
                return View();

        }

        public ActionResult MakaleYorumBR(string yorumid, string islem, string id)
        {
            if (Session["uye"] != null)
            {
                int yeniid = 0;
                if (int.TryParse(yorumid, out yeniid))
                {
                    List<Uyeler> uye = (List<Uyeler>)Session["uye"];


                    HttpCookie cookie = Request.Cookies[uye[0].uyeid.ToString()];
                    if (cookie.Values != null)
                    {
                        foreach (var item in cookie.Values.GetValues("yorumid"))
                        {
                            if (item == yeniid.ToString())
                                return RedirectToAction("MakaleDetay", new { id = id });
                        }
                    }
                    Yorumlar yorum = context.Yorumlars.Where(x => x.yorumid == yeniid).Take(1).FirstOrDefault();
                    if (yorum != null)
                    {
                        if (islem == "Red")
                            yorum.yorumred++;
                        else if (islem == "Begeni")
                            yorum.yorumbegeni++;
                        context.SaveChanges();

                        cookie.Values.Add("yorumid", yeniid.ToString());
                        cookie.Expires.Add(TimeSpan.FromDays(7));
                        Response.Cookies.Add(cookie);
                    }
                }
            }

            return RedirectToAction("MakaleDetay", new { id = id });
        }

        public ActionResult MakaleYorumYaz(string yorumyazari = null, string yorummail = null,string makaleid=null, string yorumicerik = null, string hebardarolmak = null)
        {
            if (Session["uye"] != null)
            {
                int yeniid;
                if (int.TryParse(makaleid, out yeniid) && yorumyazari != null && yorummail != null && yorumicerik != null)
                {
                    Yorumlar yorum = new Yorumlar();
                    yorum.yorumdurum = false;
                    yorum.yorumip = this.Request.UserHostAddress;
                    bool haberdar = false;
                    if (hebardarolmak == "on")
                        haberdar = true;
                    yorum.yorumhaberdarolmakistiyorum = haberdar;
                    yorum.yorumicerik = yorumicerik;
                    yorum.yorummail = yorummail;
                    yorum.yorummakaleID = yeniid;
                    yorum.yorumtarihi = DateTime.Now;
                    yorum.yorumyazari = yorumyazari;
                    yorum.yorumbegeni = 0;
                    yorum.yorumred = 0;
                    context.Yorumlars.Add(yorum);
                    context.SaveChanges();
                    List<Yorumlar> yorumlar = (List<Yorumlar>)context.Yorumlars.Where(x => x.yorummakaleID == yeniid).ToList();
                }
            }

            return RedirectToAction("MakaleDetay", new { id =makaleid });
        }

        #endregion




    }
}
