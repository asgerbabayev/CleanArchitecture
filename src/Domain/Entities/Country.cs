namespace CleanArchitecture.Domain.Entities;
public class Country : BaseAuditableEntity
{
    public string Name { get; set; } = null!;
    public string? PhoneAreaCode { get; set; }

    public virtual ICollection<City>? Cities { get; set; }
}
