using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Areas.Dashboard.Entities;

namespace Infrastructure.Data.Config
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            // Admin
            builder.HasMany(a => a.Roles).WithMany(r => r.Admins);
            builder.HasData(
                new Admin 
                {
                    Id=1, FirstName="عبدالرحمن", LastName="اسامة",
                    Email="eslamboully@gmail.com", Password = "$2y$12$rt2vHotyKtNEx.lWp.F9Yemtmbq2mu9F5pZNYHaWsyD8ctKLYUGda"
                }
            );
            // Admin Validation
            builder.Property(a => a.FirstName).IsRequired().HasMaxLength(180);
            builder.Property(a => a.LastName).IsRequired().HasMaxLength(180);
            builder.Property(a => a.Email).IsRequired().HasMaxLength(180);
            builder.Property(a => a.Password).IsRequired().HasMaxLength(180);
        }
    }
}