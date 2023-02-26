using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Infrastructure.Persistence.Configuration;

//public class BaseAudiTableEntityConfiguration<T, Schema> : IEntityTypeConfiguration<T> 
//    where T : BaseAuditableEntity 
//    where Schema: IEquatable<string>
public class BaseAudiTableEntityConfiguration<T> : IEntityTypeConfiguration<T>
    where T : BaseAuditableEntity
{
    public virtual void Configure(EntityTypeBuilder<T> entity)
    {
        //entity.ToTable(typeof(T).Name, nameof(Schema));

        entity.Property(p => p.CreatedDate).IsRequired();  //     -> zorunlu alan
        entity.Property(p => p.CreatedBy).HasMaxLength(100).IsRequired();

        entity.Property(p => p.LastModifiedTime).IsRequired(false);
        entity.Property(p => p.LastModifiedBy).HasMaxLength(100).IsRequired(false);
    }
}