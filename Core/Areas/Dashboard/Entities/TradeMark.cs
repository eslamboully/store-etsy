using System.Collections.Generic;
using System.Linq;

namespace Core.Areas.Dashboard.Entities
{
    public class TradeMark
    {
        public int Id { get; set; }
        public string Logo { get; set; }
        public virtual IList<TradeMarkTranslation> Translations { get; set; }
        
        public TradeMarkTranslation Translatable(string locale)
        {
            var row = Translations.SingleOrDefault(t => t.Locale == locale);
            return row;
        }
    }
}