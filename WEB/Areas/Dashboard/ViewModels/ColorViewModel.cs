using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Core.Areas.Dashboard.Entities;
using Microsoft.AspNetCore.Http;

namespace WEB.Areas.Dashboard.ViewModels
{
    public class ColorViewModel
    {
        public string Degree { get; set; }
        public IList<ColorTranslation> Translations { get; set; }
    }
}