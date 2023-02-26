using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Infrastructure.Persistence.Configuration;

public class CityConfiguration : BaseAudiTableEntityConfiguration<City>
{
    public override void Configure(EntityTypeBuilder<City> entity)
    {

        entity.ToTable("Cities", "Setting");

        //entity.HasKey(e => e.Id); -> primary key
        entity.Property(p => p.Name).HasMaxLength(256).IsRequired();
        entity.Property(p => p.PhoneCode).HasMaxLength(50).IsRequired(false);

        entity.HasOne(c => c.Country)
            .WithMany(c => c.Cities)
            .HasForeignKey(c => c.CountryId);

        base.Configure(entity);
    }
}
