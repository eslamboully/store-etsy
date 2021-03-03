using System.Collections.Generic;
using System.Linq;

namespace Core.Areas.Dashboard.Entities
{
    public class CategoryColor
    {
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        
        public int ColorId { get; set; }
        public virtual Color Color { get; set; }

        public string Photo { get; set; }
    }
}