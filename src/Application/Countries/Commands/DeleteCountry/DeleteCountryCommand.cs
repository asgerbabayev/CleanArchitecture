using CleanArchitecture.Application.Common.Exceptions;

namespace CleanArchitecture.Application.Countries.Commands.DeleteCountry;

public record DeleteCountryCommand(int id) : IRequest { }

public class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand>
{
    private readonly IApplicationDbContext _context;
    public DeleteCountryCommandHandler(IApplicationDbContext context) => this._context = context;

    public async Task<Unit> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Countries.FindAsync(new object[] { request.id }, cancellationToken);
        if (entity == null)
        { 
            throw new NotFoundException(nameof(Country), request.id);
        }

        _context.Countries.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}