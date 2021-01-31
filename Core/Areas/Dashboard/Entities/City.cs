using System.Collections.Generic;
using System.Linq;

namespace Core.Areas.Dashboard.Entities
{
    public class City
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; }
        public IList<CityTranslation> Translations { get; set; }
        public IList<State> States { get; set; }
        public CityTranslation Translatable(string locale)
        {
            var row = Translations.SingleOrDefault(t => t.Locale == locale);
            return row;
        }
    }
}