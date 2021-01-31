using System.Collections.Generic;
using System.Linq;

namespace Core.Areas.Dashboard.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public virtual Category Parent { get; set; }
        public virtual IList<CategoryTranslation> Translations { get; set; }
        public virtual IList<Category> Childs { get; set; }
        public CategoryTranslation Translatable(string locale)
        {
            var row = Translations.SingleOrDefault(t => t.Locale == locale);
            return row;
        }
    }
}