using System.ComponentModel.DataAnnotations;
using Core.Resources;

namespace Core.Areas.Dashboard.Entities
{
    public class CountryTranslation
    {
        public int Id { get; set; }
        [Required(ErrorMessageResourceName = "Title_Required", ErrorMessageResourceType = typeof(SharedResource))]
        public string Title { get; set; }
        public string Locale { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}