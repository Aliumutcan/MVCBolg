using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcBlog.Classes.Helperlar
{
    public static class CommonHelpers
    {
        /// <summary>
        /// istemediğiniz bir alanı gizle daha sonra başka biryerde tekrar acabilirsiniz.
        /// </summary>
        /// <param name="deger">Acık mi kapalı mı</param>
        /// <param name="icerik">Gizlenecek içerik</param>
        /// <param name="id">benzersiz içerik adı</param>
        /// <returns></returns>
        public static string GizlelenKutu(this HtmlHelper helper, bool deger, string icerik, string id)
        {
            string yenideger;
            if (deger)
                yenideger = "blok";
            else
                yenideger = "none";
            return String.Format("<div id='{0}' class='display:{1};'>{2}</div>", id, yenideger, icerik);
        }
    }
}