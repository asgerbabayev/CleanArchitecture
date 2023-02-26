using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Infrastructure.Persistence;
public class ApplicationDbContextInitializer
{
    private readonly ApplicationDbContext _context;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<ApplicationDbContextInitializer> _logger;

    public ApplicationDbContextInitializer(
        ApplicationDbContext context,
        RoleManager<IdentityRole> roleManager,
        UserManager<ApplicationUser> userManager,
        ILogger<ApplicationDbContextInitializer> logger)
    {
        _logger = logger;
        _context = context;
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task InitializeAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
                await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error accurd while initializing the database");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var administratorRole = new IdentityRole("administrator");

        // db içerisinde bu rol yok ise, ekle :)
        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await _roleManager.CreateAsync(administratorRole);
        }

        var administrator = new ApplicationUser { UserName = "murat@vuranok.com", Email = "murat@vuranok.com" };
        // db içerisinde bu kullanıcı yok ise, ekle :)
        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "Pa$$word1");
            await _userManager.AddToRolesAsync(administrator, new string[] { administratorRole.Name! });
        }
         
        // Default Data
        // Seed Countries
        if (!_context.Countries.Any())
        {
            await _context.Countries.AddRangeAsync(new List<Country>()
            {
                new Country { Name = "日本", PhoneAreaCode = "392" }
            });
        }
         
        await _context.SaveChangesAsync();
    }
}

//https://restcountries.com/