using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Resources;

namespace Core.Areas.Dashboard.Entities
{
    public class SizeTranslation
    {
        public int Id { get; set; }
        [Required(ErrorMessageResourceName = "Title_Required", ErrorMessageResourceType = typeof(SharedResource))]
        public string Title { get; set; }
        public string Locale { get; set; }
        public int SizeId { get; set; }
        public virtual Size Size { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}