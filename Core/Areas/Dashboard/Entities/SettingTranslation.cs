using System.ComponentModel.DataAnnotations;
using Core.Resources;

namespace Core.Areas.Dashboard.Entities
{
    public class SettingTranslation
    {
        public int Id { get; set; }
        [Required(ErrorMessageResourceName = "Title_Required", ErrorMessageResourceType = typeof(SharedResource))]
        public string DisplayName { get; set; }
        public string Value { get; set; }
        public string Locale { get; set; }
        public int SettingId { get; set; }
        public virtual Setting Setting { get; set; }
    }
}