using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Areas.Dashboard.Entities
{
    public class Product
    {
        public int Id { get; set; }
        
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        
        public double Price { get; set; }
        public double PriceOffer { get; set; }
        
        public int Stock { get; set; }
        
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public DateTime StartOfferAt { get; set; }
        public DateTime EndOfferAt { get; set; }
        
        public byte Status { get; set; }
        public string Reason { get; set; }
        
        public int WeightString { get; set; }
        public int WeightId { get; set; }
        public Weight Weight { get; set; }
        
        public int SizeString { get; set; }
        public List<Size> Sizes { get; set; }

        public List<Country> Countries { get; set; }
        
        public List<ManuFact> ManuFacts { get; set; }
        
        public List<Color> Colors { get; set; }

        public string Photo { get; set; }

        public int TradeMarkId { get; set; }
        public TradeMark TradeMark { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public IList<ProductTranslation> Translations { get; set; }
        
        public ProductTranslation Translatable(string locale)
        {
            var row = Translations.SingleOrDefault(t => t.Locale == locale);
            return row;
        }
    }
}