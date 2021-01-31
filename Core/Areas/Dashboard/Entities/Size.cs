using System.Collections.Generic;
using System.Linq;

namespace Core.Areas.Dashboard.Entities
{
    public class Size
    {
        public int Id { get; set; }
        public virtual IList<SizeTranslation> Translations { get; set; }
        
        public SizeTranslation Translatable(string locale)
        {
            var row = Translations.SingleOrDefault(t => t.Locale == locale);
            return row;
        }
    }
}