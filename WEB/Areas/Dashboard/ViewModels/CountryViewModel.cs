using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Core.Areas.Dashboard.Entities;
using Microsoft.AspNetCore.Http;
using WEB.Resources;

namespace WEB.Areas.Dashboard.ViewModels
{
    public class CountryViewModel
    {
        [Required(ErrorMessageResourceName = "Mob_Required", ErrorMessageResourceType = typeof(SharedResource))]
        public string Mob { get; set; }
        [Required(ErrorMessageResourceName = "Code_Required", ErrorMessageResourceType = typeof(SharedResource))]
        public string Code { get; set; }
        // [FileExtensions(Extensions = "jpg,png,jpeg",ErrorMessage = "jpg, png, jpeg")]
        public IFormFile Logo { get; set; }
        
        // public string Title { get; set; }
        // public string Description { get; set; }
        // public string Locale { get; set; }
        // public int CountryId { get; set; }
        public IList<CountryTranslation> Translations { get; set; }
    }
}