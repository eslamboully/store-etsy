
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Core.Areas.Dashboard.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public string Mob { get; set; }
        public string Code { get; set; }
        public string Logo { get; set; }
        public IList<CountryTranslation> Translations { get; set; }
        public IList<City> Cities { get; set; }
        public IList<State> States { get; set; }
        public CountryTranslation Translatable(string locale)
        {
            var row = Translations.SingleOrDefault(t => t.Locale == locale);
            return row;
        }
    }
}