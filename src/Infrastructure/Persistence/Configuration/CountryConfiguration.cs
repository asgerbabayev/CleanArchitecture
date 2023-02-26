using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Infrastructure.Persistence.Configuration;
//public class CountryConfiguration : IEntityTypeConfiguration<Country>
//{
//    public void Configure(EntityTypeBuilder<Country> entity)
//    {
//        entity.ToTable("Countries", "Setting");

//        //entity.HasKey(e => e.Id); -> primary key
//        entity.Property(p => p.Name).HasMaxLength(256).IsRequired();
//        entity.Property(p => p.PhoneAreaCode).HasMaxLength(50).IsRequired(false);

//    }
//}
//public class CityConfiguration : IEntityTypeConfiguration<City>
//{
//    public void Configure(EntityTypeBuilder<City> entity)
//    {
//        entity.ToTable("Cities", "Setting");

//        //entity.HasKey(e => e.Id); -> primary key
//        entity.Property(p => p.Name).HasMaxLength(256).IsRequired();
//        entity.Property(p => p.PhoneCode).HasMaxLength(50).IsRequired(false);


//        //entity.Property(p => p.CreatedDate).IsRequired(false);  -> boş geçilebilir.


//        entity.HasOne(c => c.Country)
//            .WithMany(c => c.Cities)
//            .HasForeignKey(c => c.CountryId);
//    }
//}



public class CountryConfiguration : BaseAudiTableEntityConfiguration<Country>
{
    public override void Configure(EntityTypeBuilder<Country> entity)
    {
        entity.ToTable("Countries", "Setting");

        //entity.HasKey(e => e.Id); -> primary key
        entity.Property(p => p.Name).HasMaxLength(256).IsRequired();
        entity.Property(p => p.PhoneAreaCode).HasMaxLength(50).IsRequired(false);

        base.Configure(entity);
    }
}
