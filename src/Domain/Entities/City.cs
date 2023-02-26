namespace CleanArchitecture.Domain.Entities; 
public class City : BaseAuditableEntity
{
    public string Name { get; set; } = null!;
    public string? PhoneCode { get; set; }
     
    public int CountryId { get; set; }
    public Country Country { get; set; } = null!;
}