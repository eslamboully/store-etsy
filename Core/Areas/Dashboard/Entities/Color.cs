using System.Collections.Generic;
using System.Linq;

namespace Core.Areas.Dashboard.Entities
{
    public class Color
    {
        public int Id { get; set; }
        public string Degree { get; set; }
        public virtual IList<ColorTranslation> Translations { get; set; }
        
        public ColorTranslation Translatable(string locale)
        {
            var row = Translations.SingleOrDefault(t => t.Locale == locale);
            return row;
        }
    }
}