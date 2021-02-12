using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text.Json.Serialization;
using Core.Areas.Dashboard.Entities;
using Microsoft.AspNetCore.Http;

namespace WEB.Areas.Dashboard.ViewModels
{
    public class CategoryViewModel
    {
        public IList<CategoryTranslation> Translations { get; set; }
        public int? ParentId { get; set; }
        public IFormFile Photo { get; set; }
        public string PhotoName { get; set; }
    }
}