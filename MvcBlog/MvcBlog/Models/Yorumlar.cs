using System;
using System.Collections.Generic;

namespace MvcBlog.Models
{
    public partial class Yorumlar
    {
        public int yorumid { get; set; }
        public string yorumyazari { get; set; }
        public string yorummail { get; set; }
        public string yorumicerik { get; set; }
        public Nullable<System.DateTime> yorumtarihi { get; set; }
        public Nullable<int> yorummakaleID { get; set; }
        public Nullable<bool> yorumhaberdarolmakistiyorum { get; set; }
        public Nullable<short> yorumbegeni { get; set; }
        public Nullable<short> yorumred { get; set; }
        public string yorumip { get; set; }
        public Nullable<bool> yorumdurum { get; set; }
        public virtual Makaleler Makaleler { get; set; }
    }
}
