using System.Collections.Generic;
using System.Linq;

namespace Core.Areas.Dashboard.Entities
{
    public class Weight
    {
        public int Id { get; set; }
        public virtual IList<WeightTranslation> Translations { get; set; }
        
        public WeightTranslation Translatable(string locale)
        {
            var row = Translations.SingleOrDefault(t => t.Locale == locale);
            return row;
        }
    }
}