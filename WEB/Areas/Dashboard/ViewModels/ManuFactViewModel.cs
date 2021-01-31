using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Core.Areas.Dashboard.Entities;
using Microsoft.AspNetCore.Http;

namespace WEB.Areas.Dashboard.ViewModels
{
    public class ManuFactViewModel
    {
        [Required]
        public string ContactName { get; set; }
        [Required]
        public string Phone { get; set; }
        public IFormFile Logo { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Lng { get; set; }
        public string Lat { get; set; }
        public IList<ManuFactTranslation> Translations { get; set; }
    }
}