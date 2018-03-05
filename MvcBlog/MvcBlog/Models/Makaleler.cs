using System;
using System.Collections.Generic;

namespace MvcBlog.Models
{
    public partial class Makaleler
    {
        public Makaleler()
        {
            this.Yorumlars = new List<Yorumlar>();
            this.Etiteklers = new List<Etitekler>();
        }

        public int makaleid { get; set; }
        public string makalebaslik { get; set; }
        public Nullable<int> makalekapakresimID { get; set; }
        public string makaleicerik { get; set; }
        public Nullable<int> makaleyazarID { get; set; }
        public Nullable<int> makalekategoriID { get; set; }
        public Nullable<System.DateTime> makaleyayintarihi { get; set; }
        public Nullable<byte> makalebegeni { get; set; }
        public Nullable<short> makalebegenenkisisayisi { get; set; }
        public Nullable<bool> makaledurum { get; set; }
        public string makalehostname { get; set; }
        public Nullable<int> gorunum { get; set; }
        public virtual Galeri Galeri { get; set; }
        public virtual Kategoriler Kategoriler { get; set; }
        public virtual Yazarlar Yazarlar { get; set; }
        public virtual ICollection<Yorumlar> Yorumlars { get; set; }
        public virtual ICollection<Etitekler> Etiteklers { get; set; }
    }
}
