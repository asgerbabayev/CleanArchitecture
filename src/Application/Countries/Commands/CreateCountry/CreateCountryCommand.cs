namespace CleanArchitecture.Application.Countries.Commands.CreateCountry;
public class CreateCountryCommand : IRequest<int>
{
    public string? Name { get; set; }
    public string? PhoneAreaCode { get; set; }
}

public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, int>
{
    private readonly IApplicationDbContext _context;
    public CreateCountryCommandHandler(IApplicationDbContext context) => this._context = context;
     
    public async Task<int> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
    {
        //var entity = new Country() { };

        Country entity = new()
        {
            Name = request.Name!,
            PhoneAreaCode = request.PhoneAreaCode,
        };

        await _context.Countries.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity.Id;
    }
}

//https://davecallan.com/how-to-use-hilo-with-entity-framework/