using System;
using System.Collections.Generic;

namespace MvcBlog.Models
{
    public partial class Yazarlar
    {
        public Yazarlar()
        {
            this.Galeris = new List<Galeri>();
            this.Makalelers = new List<Makaleler>();
        }

        public int yazarid { get; set; }
        public string yazaradi { get; set; }
        public string yazarsoyadi { get; set; }
        public string yazarmail { get; set; }
        public string yazarsifre { get; set; }
        public string yazartel { get; set; }
        public Nullable<int> yazarresimID { get; set; }
        public string yazarhakkinda { get; set; }
        public string yazaradres { get; set; }
        public string yazarTC { get; set; }
        public Nullable<System.DateTime> yazartarihi { get; set; }
        public virtual ICollection<Galeri> Galeris { get; set; }
        public virtual ICollection<Makaleler> Makalelers { get; set; }
    }
}
