using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Core.Areas.Dashboard.Entities;
using Microsoft.AspNetCore.Http;

namespace WEB.Areas.Dashboard.ViewModels
{
    public class SizeViewModel
    {
        public IList<SizeTranslation> Translations { get; set; }
    }
}