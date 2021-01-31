using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Areas.Dashboard.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB.Resources;

namespace WEB.Areas.Dashboard.ViewModels
{
    public class StateViewModel
    {
        [Required(ErrorMessageResourceName = "Country_Required", ErrorMessageResourceType = typeof(SharedResource))]
        public int CountryId { get; set; }
        [Required(ErrorMessageResourceName = "City_Required", ErrorMessageResourceType = typeof(SharedResource))]
        public int CityId { get; set; }
        public IList<Country> Countries { get; set; }
        public IList<City> Cities { get; set; }
        public IList<StateTranslation> Translations { get; set; }
    }
}