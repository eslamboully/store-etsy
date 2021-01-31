using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Core.Areas.Dashboard.Entities;

namespace Infrastructure.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            //
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.UseCollation("Latin1_General_100_CI_AI_SC_UTF8");
            // Countries
            modelBuilder.Entity<Country>().HasMany(c => c.Translations)
                .WithOne(c => c.Country)
                .HasForeignKey(c => c.CountryId)
                .OnDelete(DeleteBehavior.Cascade);
            // Cities
            modelBuilder.Entity<City>().HasOne(c => c.Country)
                .WithMany(c => c.Cities)
                .HasForeignKey(c=>c.CountryId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<City>().HasMany(c => c.Translations)
                .WithOne(c => c.City)
                .HasForeignKey(c => c.CityId)
                .OnDelete(DeleteBehavior.Cascade);
            // States
            modelBuilder.Entity<State>().HasMany(c => c.Translations)
                .WithOne(c => c.State)
                .HasForeignKey(c => c.StateId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<State>().HasOne(c => c.Country)
                .WithMany(c => c.States)
                .HasForeignKey(c => c.CountryId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<State>().HasOne(c => c.City)
                .WithMany(c => c.States)
                .HasForeignKey(c => c.CityId)
                .OnDelete(DeleteBehavior.Cascade);
            // Categories
            modelBuilder.Entity<Category>().HasMany(c => c.Translations)
                .WithOne(c => c.Category)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Category>().HasMany(c => c.Childs)
                .WithOne(c => c.Parent)
                .HasForeignKey(c => c.ParentId);
            // TradeMarks
            modelBuilder.Entity<TradeMark>().HasMany(c => c.Translations)
                .WithOne(c => c.TradeMark)
                .HasForeignKey(c => c.TradeMarkId)
                .OnDelete(DeleteBehavior.Cascade);
            // ManuFacts
            modelBuilder.Entity<ManuFact>().HasMany(c => c.Translations)
                .WithOne(c => c.ManuFact)
                .HasForeignKey(c => c.ManuFactId)
                .OnDelete(DeleteBehavior.Cascade);
            // Shippings
            modelBuilder.Entity<Shipping>().HasMany(c => c.Translations)
                .WithOne(c => c.Shipping)
                .HasForeignKey(c => c.ShippingId)
                .OnDelete(DeleteBehavior.Cascade);
            // Users
            modelBuilder.Entity<User>().HasMany(c => c.Shippings)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            // Malls
            modelBuilder.Entity<Mall>().HasMany(c => c.Translations)
                .WithOne(c => c.Mall)
                .HasForeignKey(c => c.MallId)
                .OnDelete(DeleteBehavior.Cascade);
            // Colors
            modelBuilder.Entity<Color>().HasMany(c => c.Translations)
                .WithOne(c => c.Color)
                .HasForeignKey(c => c.ColorId)
                .OnDelete(DeleteBehavior.Cascade);
            // Sizes
            modelBuilder.Entity<Size>().HasMany(c => c.Translations)
                .WithOne(c => c.Size)
                .HasForeignKey(c => c.SizeId)
                .OnDelete(DeleteBehavior.Cascade);
            // Weights
            modelBuilder.Entity<Weight>().HasMany(c => c.Translations)
                .WithOne(c => c.Weight)
                .HasForeignKey(c => c.WeightId)
                .OnDelete(DeleteBehavior.Cascade);
            // Settings
            modelBuilder.Entity<Setting>().HasMany(c => c.Translations)
                .WithOne(c => c.Setting)
                .HasForeignKey(c => c.SettingId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        // schemes authentication
        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        // countries
        public DbSet<Country> Countries { get; set; }
        public DbSet<CountryTranslation> CountryTranslations { get; set; }
        // cities
        public DbSet<City> Cities { get; set; }
        public DbSet<CityTranslation> CityTranslations { get; set; }
        // states
        public DbSet<State> States { get; set; }
        public DbSet<StateTranslation> StateTranslations { get; set; }
        // categories
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryTranslation> CategoryTranslations { get; set; }
        // TradeMarks
        public DbSet<TradeMark> TradeMarks { get; set; }
        public DbSet<TradeMarkTranslation> TradeMarkTranslations { get; set; }
        // ManuFacts
        public DbSet<ManuFact> ManuFacts { get; set; }
        public DbSet<ManuFactTranslation> ManuFactTranslations { get; set; }
        // Shippings
        public DbSet<Shipping> Shippings { get; set; }
        public DbSet<ShippingTranslation> ShippingTranslations { get; set; }
        // Malls
        public DbSet<Mall> Malls { get; set; }
        public DbSet<MallTranslation> MallTranslations { get; set; }
        // Colors
        public DbSet<Color> Colors { get; set; }
        public DbSet<ColorTranslation> ColorTranslations { get; set; }
        // Sizes
        public DbSet<Size> Sizes { get; set; }
        public DbSet<SizeTranslation> SizeTranslations { get; set; }
        // Weights
        public DbSet<Weight> Weights { get; set; }
        public DbSet<WeightTranslation> WeightTranslations { get; set; }
        // Products
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductTranslation> ProductTranslations { get; set; }

        // Settings
        public DbSet<Setting> Settings { get; set; }
        public DbSet<SettingTranslation> SettingTranslations { get; set; }
    }
}