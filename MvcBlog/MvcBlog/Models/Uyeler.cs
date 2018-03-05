using System;
using System.Collections.Generic;

namespace MvcBlog.Models
{
    public partial class Uyeler
    {
        public int uyeid { get; set; }
        public string uyeadi { get; set; }
        public string uyesoyadi { get; set; }
        public string uyemail { get; set; }
        public string uyesifre { get; set; }
        public string uyetanima { get; set; }
        public Nullable<System.DateTime> uyekayittarihi { get; set; }
    }
}
