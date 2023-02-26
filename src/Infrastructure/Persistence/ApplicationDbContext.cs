using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using System.Reflection;

namespace CleanArchitecture.Infrastructure.Persistence;
public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext
{
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;
    public ApplicationDbContext(
        DbContextOptions options,
        IOptions<OperationalStoreOptions> operationalStoreOptions,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor
        ) : base(options, operationalStoreOptions)
    {
        this._auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    public DbSet<Country> Countries { get; set; }
    public DbSet<City> Cities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    } 

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // builder.ApplyConfiguration<Country>(new CountryConfiguration());
        // builder.ApplyConfiguration<City>(new CityConfiguration());
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
