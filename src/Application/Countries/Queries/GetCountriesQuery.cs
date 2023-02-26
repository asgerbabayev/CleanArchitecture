namespace CleanArchitecture.Application.Countries.Queries;

public class GetCountriesQuery : IRequest<CountryVM> { }
public class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, CountryVM>
{
    #region Constructor
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;
    public GetCountriesQueryHandler(
        IMapper mapper,
        IApplicationDbContext context)
    {
        this._mapper = mapper;
        this._context = context;
    }
    #endregion

    public async Task<CountryVM> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
    {
        return new CountryVM
        {
            Countries = await _context.Countries.AsNoTracking()
            .ProjectTo<CountryDto>(_mapper.ConfigurationProvider)
            .OrderBy(x => x.Name)
            .ToListAsync()
        };
    }
}
