using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text.Json.Serialization;
using Core.Areas.Dashboard.Entities;
using Microsoft.AspNetCore.Http;
using WEB.Resources;

namespace WEB.Areas.Dashboard.ViewModels
{
    public class ProductViewModel
    {
        public IList<ProductTranslation> Translations { get; set; }
        public IFormFile Photo { get; set; }
        public string PhotoName { get; set; }
        public int CategoryId { get; set; }
        // [Required(ErrorMessageResourceName = "Price_Required", ErrorMessageResourceType = typeof(SharedResource))]
        public double Price { get; set; }
        public double PriceOffer { get; set; }
        public DateTime StartOfferAt { get; set; }
        public DateTime EndOfferAt { get; set; }
        public byte Status { get; set; }
        public string Reason { get; set; }
        // [Required(ErrorMessageResourceName = "SizeString_Required", ErrorMessageResourceType = typeof(SharedResource))]
        public string SizeString { get; set; }
        public ICollection<Size> Sizes { get; set; }
        public ICollection<Country> Countries { get; set; }
        public ICollection<Color> Colors { get; set; }
        [Required]
        [MaxLength(1)]
        public List<int> SizesArray { get; set; }
        [Required]
        [MaxLength(1)]
        public List<int> CountriesArray { get; set; }
        [Required]
        [MaxLength(1)]
        public List<int> ColorsArray { get; set; }
        public int UserId { get; set; }
    }
}