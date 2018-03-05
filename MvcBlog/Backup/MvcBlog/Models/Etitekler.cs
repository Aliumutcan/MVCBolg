using System;
using System.Collections.Generic;

namespace MvcBlog.Models
{
    public partial class Etitekler
    {
        public Etitekler()
        {
            this.Makalelers = new List<Makaleler>();
        }

        public int etiketid { get; set; }
        public string etiketadi { get; set; }
        public virtual ICollection<Makaleler> Makalelers { get; set; }
    }
}
