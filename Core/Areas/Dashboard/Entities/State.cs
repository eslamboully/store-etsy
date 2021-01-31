using System.Collections.Generic;
using System.Linq;

namespace Core.Areas.Dashboard.Entities
{
    public class State
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        
        public Country Country { get; set; }
        public City City { get; set; }
        public IList<StateTranslation> Translations { get; set; }
        
        public StateTranslation Translatable(string locale)
        {
            var row = Translations.SingleOrDefault(t => t.Locale == locale);
            return row;
        }
    }
}