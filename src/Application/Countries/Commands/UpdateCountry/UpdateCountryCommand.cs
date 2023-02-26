namespace CleanArchitecture.Application.Countries.Commands.UpdateCountry;
public class UpdateCountryCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? PhoneAreaCode { get; set; }
}
