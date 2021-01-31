using System.Collections.Generic;
using System.Linq;

namespace Core.Areas.Dashboard.Entities
{
    public class ManuFact
    {
        public int Id { get; set; }
        public string ContactName { get; set; }
        public string Phone { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Logo { get; set; }
        public string Lng { get; set; }
        public string Lat { get; set; }
        public virtual IList<ManuFactTranslation> Translations { get; set; }
        
        public ManuFactTranslation Translatable(string locale)
        {
            var row = Translations.SingleOrDefault(t => t.Locale == locale);
            return row;
        }
    }
}