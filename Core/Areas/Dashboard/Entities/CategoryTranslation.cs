using System.ComponentModel.DataAnnotations;
using Core.Resources;

namespace Core.Areas.Dashboard.Entities
{
    public class CategoryTranslation
    {
        public int Id { get; set; }
        [Required(ErrorMessageResourceName = "Title_Required", ErrorMessageResourceType = typeof(SharedResource))]
        public string Title { get; set; }
        [Required(ErrorMessageResourceName = "Description_Required", ErrorMessageResourceType = typeof(SharedResource))]
        public string Description { get; set; }
        public string Words { get; set; }
        public string Locale { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}