using MvcBlog.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;


namespace MvcBlog.Controllers.Yoneticilere
{
    public class GenelController : Controller
    {
        //
        // GET: /Genel/

        public ActionResult Index()
        {
            return View();
        }
        public BlogDBContext context = new BlogDBContext();

        public int MakaleGorunum(int id)
        {
            string yeniid = Convert.ToString(id);
            try
            {
                if (Request.Cookies["makaleidler"] != null)
                {
                    HttpCookie cookie = Response.Cookies["makaleidler"];
                    string makaleler = cookie.Value;
                    string[] makaledizi = makaleler.Split(',');

                    foreach (var item in makaledizi)
                    {
                        if (item == yeniid)
                            return 0;
                        else
                        {
                            cookie.Value = cookie.Value + "," + yeniid;
                            cookie.Expires = DateTime.Now.AddYears(1);
                            Response.Cookies.Add(cookie);
                            break;
                        }
                    }
                }
            }
            catch (Exception hata)
            {
                HttpCookie cookie1 = new HttpCookie("makaleidler", yeniid);
                cookie1.Expires = DateTime.Now.AddDays(1);
                Request.Cookies.Add(cookie1);
            }

            BlogDBContext con = new BlogDBContext();
            Makaleler makale = con.Makalelers.Where(x => x.makaleid == id).Take(1).FirstOrDefault();
            makale.gorunum++;
            con.SaveChanges();
            return 1;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="yorumlar"></param>
        /// <param name="id">bu id deki yorum mailline mail gönderilmeyecek</param>
        public static void MakaleYorumHaberVer(List<Yorumlar> yorumlar, int id)
        {
            if (yorumlar.Count > 0)
            {
                foreach (var item in yorumlar)
                {
                    if (item.yorumid != id && item.yorumdurum == true)
                    {
                        WebMail.SmtpServer = "smtp.gmail.com";
                        WebMail.UserName = "aliumutcankul05@gmail.com";
                        WebMail.Password = "annebaba005";
                        WebMail.EnableSsl = true;
                        WebMail.SmtpPort = 587;
                        string mesajicerigi = string.Format("www.aliumutcan.com/Makale/Detay/{0}/{1} linkindeki makaleye yeni bir yorum eklendi ", item.yorummakaleID, GenelController.KarakterDuzenle(item.Makaleler.makalebaslik));
                        WebMail.Send(item.yorummail, "Ali Umutcan'nın blogundaki makaleye yeni bir yorum eklendi", mesajicerigi, "aliumutcankul05@gmail.com");

                    }
                }
            }

        }

        public static string KarakterDuzenle(string metin)
        {
            string Temp = metin.ToLower();
            Temp = Temp.Replace("-", ""); Temp = Temp.Replace(" ", "-");
            Temp = Temp.Replace("ç", "c"); Temp = Temp.Replace("g", "g");
            Temp = Temp.Replace("i", "i"); Temp = Temp.Replace("ö", "o");
            Temp = Temp.Replace("s", "s"); Temp = Temp.Replace("ü", "u");
            Temp = Temp.Replace("\"", ""); Temp = Temp.Replace("/", "");
            Temp = Temp.Replace("(", ""); Temp = Temp.Replace(")", "");
            Temp = Temp.Replace("{", ""); Temp = Temp.Replace("}", "");
            Temp = Temp.Replace("%", ""); Temp = Temp.Replace("&", "");
            Temp = Temp.Replace("+", ""); Temp = Temp.Replace(".", "");
            Temp = Temp.Replace("?", ""); Temp = Temp.Replace(",", "");
            Temp = Temp.Replace("'", "-"); Temp = Temp.Replace("!", "");
            Temp = Temp.Replace("ı", "i");
            return Temp;
        }

        public static string DurumControl(bool durum)
        {
            if (durum == true) { return KarakterDuzenle("Açık"); }
            else { return KarakterDuzenle("Kapalı"); }
        }

        public bool Etiketlerikaydet(string etiketler, Makaleler makale)
        {
            try
            {
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
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public int ResimKaydet(HttpPostedFileBase resim, int yazarid)
        {

            Galeri galeri;

            try
            {
                int kucukwidth = Convert.ToInt32(ConfigurationManager.AppSettings["kw"]);
                int kucukheight = Convert.ToInt32(ConfigurationManager.AppSettings["kh"]);
                int ortawidth = Convert.ToInt32(ConfigurationManager.AppSettings["ow"]);
                int ortaheight = Convert.ToInt32(ConfigurationManager.AppSettings["oh"]);
                int buyukwidth = Convert.ToInt32(ConfigurationManager.AppSettings["bw"]);
                int buyukheight = Convert.ToInt32(ConfigurationManager.AppSettings["bh"]);

                string resimad = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString()
                + DateTime.Now.Second.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Hour.ToString() + Path.GetExtension(resim.FileName);

                //      if (Model.Picture1 != null)
                //{
                //   string imageBase64 = Convert.ToBase64String(Model.Picture1);
                //   string imageSrc = string.Format("data:image/gif;base64,{0}", imageBase64);
                //   <img src="@imageSrc" width="100" height="100" />
                //}
                WebImage resimduzenle = new WebImage(resim.InputStream);
                resimduzenle.Resize(kucukwidth, kucukheight, false, true);
                resimduzenle.FileName = resimad + "_kucuk";
                byte[] kucukresim = resimduzenle.GetBytes();


                resimduzenle.Resize(ortawidth, ortaheight, false, true);
                resimduzenle.FileName = resimad + "_orta";
                byte[] ortaresim = resimduzenle.GetBytes();


                resimduzenle.Resize(buyukwidth, buyukheight, false, false);
                resimduzenle.FileName = resimad + "_buyuk";
                byte[] buyukresim = resimduzenle.GetBytes();

                //Image orjres = Image.FromStream(resim.InputStream);

                //Bitmap kr = new Bitmap(orjres, kucukwidth, kucukheight);
                //Bitmap or = new Bitmap(orjres, ortawidth, ortaheight);
                //Bitmap br = new Bitmap(orjres, buyukwidth, buyukheight);

                //kr.Save(Server.MapPath("~/Content/resimler/kucuk/") + resimad);
                //or.Save(Server.MapPath("~/Content/resimler/orta/") + resimad);
                //br.Save(Server.MapPath("~/Content/resimler/buyuk/") + resimad);


                galeri = new Galeri();

                galeri.ekleyenID = yazarid;
                galeri.adi = resimad;
                galeri.eklemetarihi = DateTime.Now;
                galeri.resimtipi = resimduzenle.GetType().ToString();
                galeri.kucukresimimage = kucukresim;
                galeri.ortaresimimage = ortaresim;
                galeri.buyukresimimage = buyukresim;


                context.Galeris.Add(galeri);
                context.SaveChanges();

                return galeri.galeriid;

            }
            catch (Exception)
            {
                return 0;
            }

        }

        private static string Sifrele(string metin, HashAlgorithm alg)
        {
            byte[] byteDegeri = System.Text.Encoding.UTF8.GetBytes(metin);
            byte[] sifreliByte = alg.ComputeHash(byteDegeri);
            return Convert.ToBase64String(sifreliByte);
        }

        public static string MD5eDonustur(string metin)
        {
            MD5CryptoServiceProvider pwd = new MD5CryptoServiceProvider();
            return Sifrele(metin, pwd);
        }

        public enum aylar
        {
            Ocak=1,
            Şubat=2,
            Mart=3,
            Nisan=4,
            Mayıs=5,
            Haziran=6,
            Temmuz=7,
            Ağustos=8,
            Eylül=9,
            Ekim=10,
            Kasım=11,
            Aralık=12
        }


    }
}
