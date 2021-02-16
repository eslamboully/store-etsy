using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text.Json.Serialization;
using Core.Areas.Dashboard.Entities;
using Microsoft.AspNetCore.Http;
using Core.Resources;

namespace WEB.Areas.Dashboard.ViewModels
{
    public class ProductPhotoViewModel
    {
        public int Id { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
        public string CategoryPhoto { get; set; }
        public string OldPhoto { get; set; }
    }
}