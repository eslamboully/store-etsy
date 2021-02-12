using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Core.Areas.Dashboard.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public double Price { get; set; } 
        public double PriceOffer { get; set; } 
        public DateTime StartOfferAt { get; set; } 
        public DateTime EndOfferAt { get; set; } 
        public byte Status { get; set; }
        public string Reason { get; set; } 
        public string SizeString { get; set; } 
        public virtual List<Size> Sizes { get; set; } 
        public virtual List<Country> Countries { get; set; } 
        public virtual List<Color> Colors { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public string Photo { get; set; }
        public IList<ProductTranslation> Translations { get; set; }
        public ProductTranslation Translatable(string locale)
        {
            var row = Translations.SingleOrDefault(t => t.Locale == locale);
            return row;
        }
    }
}