using System.Collections.Generic;
using System.Linq;

namespace Core.Areas.Dashboard.Entities
{
    public class ProductColor
    {
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        
        public int ColorId { get; set; }
        public virtual Color Color { get; set; }

        public string Photo { get; set; }
    }
}