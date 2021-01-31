using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Areas.Dashboard.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB.Resources;

namespace WEB.Areas.Dashboard.ViewModels
{
    public class CityViewModel
    {
        [Required(ErrorMessageResourceName = "Country_Required", ErrorMessageResourceType = typeof(SharedResource))]
        public int CountryId { get; set; }
        public IList<Country> Countries { get; set; }
        public IList<CityTranslation> Translations { get; set; }
    }
}