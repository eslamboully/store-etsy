using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Core.Areas.Dashboard.Entities;
using Microsoft.AspNetCore.Http;

namespace WEB.Areas.Dashboard.ViewModels
{
    public class ShippingViewModel
    {
        public List<User> Users { get; set; }
        [Required]
        public int UserId { get; set; }
        public IFormFile Logo { get; set; }
        public string Address { get; set; }
        public string Lng { get; set; }
        public string Lat { get; set; }
        public List<ShippingTranslation> Translations { get; set; }
    }
}