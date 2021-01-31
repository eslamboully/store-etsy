using System.ComponentModel.DataAnnotations;
using Core.Resources;

namespace Core.Areas.Dashboard.Entities
{
    public class ShippingTranslation
    {
        public int Id { get; set; }
        [Required(ErrorMessageResourceName = "Title_Required", ErrorMessageResourceType = typeof(SharedResource))]
        public string Title { get; set; }
        public string Locale { get; set; }
        public int ShippingId { get; set; }
        public virtual Shipping Shipping { get; set; }
    }
}