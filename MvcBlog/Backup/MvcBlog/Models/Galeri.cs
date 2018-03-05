using System;
using System.Collections.Generic;

namespace MvcBlog.Models
{
    public partial class Galeri
    {
        public Galeri()
        {
            this.Makalelers = new List<Makaleler>();
        }

        public int galeriid { get; set; }
        public string adi { get; set; }
        public string kucukresimyol { get; set; }
        public string ortaresimyol { get; set; }
        public string buyukresimyol { get; set; }
        public string videoyol { get; set; }
        public Nullable<int> ekleyenID { get; set; }
        public Nullable<System.DateTime> eklemetarihi { get; set; }
        public Nullable<int> goruntulenme { get; set; }
        public Nullable<int> begeni { get; set; }
        public byte[] kucukresimimage { get; set; }
        public byte[] ortaresimimage { get; set; }
        public byte[] buyukresimimage { get; set; }
        public string resimtipi { get; set; }
        public virtual Yazarlar Yazarlar { get; set; }
        public virtual ICollection<Makaleler> Makalelers { get; set; }
    }
}
