using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Core.Areas.Dashboard.Entities;

namespace WEB.Areas.Dashboard.ViewModels
{
    public class CategoryViewModel
    {
        public IList<CategoryTranslation> Translations { get; set; }
        public int? ParentId { get; set; }
    }
}