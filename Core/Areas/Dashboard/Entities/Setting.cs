using System.Collections.Generic;
using System.Linq;

namespace Core.Areas.Dashboard.Entities
{
    public class Setting
    {
        public int Id { get; set; }
        public IList<SettingTranslation> Translations { get; set; }
        public string Var { get; set; }
        public int Type { get; set; }
        public bool IsStatic { get; set; }
        public string StaticValue { get; set; }

        public SettingTranslation Translatable(string locale)
        {
            var row = Translations.SingleOrDefault(t => t.Locale == locale);
            return row;
        }
    }
}