using System.Collections.Generic;
using System.Linq;

namespace Core.Areas.Dashboard.Entities
{
    public class Shipping
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Address { get; set; }
        public string Logo { get; set; }
        public string Lng { get; set; }
        public string Lat { get; set; }
        public virtual User User { get; set; }
        public virtual List<ShippingTranslation> Translations { get; set; }
        
        public ShippingTranslation Translatable(string locale)
        {
            var row = Translations.SingleOrDefault(t => t.Locale == locale);
            return row;
        }
    }
}