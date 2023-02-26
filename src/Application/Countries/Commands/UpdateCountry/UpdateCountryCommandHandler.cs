using CleanArchitecture.Application.Common.Exceptions;

namespace CleanArchitecture.Application.Countries.Commands.UpdateCountry;

public class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand>
{
    private readonly IApplicationDbContext _context;
    public UpdateCountryCommandHandler(IApplicationDbContext context) => this._context = context;


    public async Task<Unit> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Countries.FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Country), request.Id);
        }

        entity.Name = request.Name;
        entity.PhoneAreaCode = request.PhoneAreaCode;

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}