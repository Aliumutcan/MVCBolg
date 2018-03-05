using System;
using System.Collections.Generic;

namespace MvcBlog.Models
{
    public partial class Kategoriler
    {
        public Kategoriler()
        {
            this.Makalelers = new List<Makaleler>();
        }

        public int kategoriid { get; set; }
        public string kategoriadi { get; set; }
        public virtual ICollection<Makaleler> Makalelers { get; set; }
    }
}
