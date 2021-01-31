using System.Collections.Generic;
using System.Linq;

namespace Core.Areas.Dashboard.Entities
{
    public class Mall
    {
        public int Id { get; set; }
        public string Owner { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public virtual IList<MallTranslation> Translations { get; set; }
        public MallTranslation Translatable(string locale)
        {
            var row = Translations.SingleOrDefault(t => t.Locale == locale);
            return row;
        }
    }
}