using System.ComponentModel.DataAnnotations;
using Core.Resources;

namespace Core.Areas.Dashboard.Entities
{
    public class ProductTranslation
    {
        public int Id { get; set; }
        [Required(ErrorMessageResourceName = "Title_Required", ErrorMessageResourceType = typeof(SharedResource))]
        public string Title { get; set; }
        public string Description { get; set; }
        public string OtherDescription { get; set; }
        public string Locale { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}