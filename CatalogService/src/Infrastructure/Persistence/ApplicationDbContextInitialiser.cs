using CatalogService.Domain.Entities;
using CatalogService.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CatalogService.Infrastructure.Persistence;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var administratorRole = new IdentityRole("Administrator");

        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await _roleManager.CreateAsync(administratorRole);
        }

        // Default users
        var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "Administrator1!");
            if (!string.IsNullOrWhiteSpace(administratorRole.Name))
            {
                await _userManager.AddToRolesAsync(administrator, new [] { administratorRole.Name });
            }
        }

        // Default data
        // Seed, if necessary
        if (!_context.Categories.Any())
        {
            

            var category = new Category { Name = "test", Image = "test" };
            _context.Categories.Add(category);
            _context.Categories.Add(new Category { Name = "test2", Image = "test2" });
            _context.Categories.Add(new Category { Name = "test3", Image = "test3", ParentCategory = category});

            _context.Items.Add(new Item()
            {
                Name = "test",
                Description = "test", 
                Image = "test",
                Category = category,
                Price = 123,
                Amount = 2
            });
            _context.Items.Add(new Item()
            {
                Name = "test2",
                Description = "test2",
                Image = "test2",
                Category = category,
                Price = 123,
                Amount = 2
            });
            _context.Items.Add(new Item()
            {
                Name = "test3",
                Description = "test3",
                Image = "test3",
                Category = category,
                Price = 123,
                Amount = 2
            });

            await _context.SaveChangesAsync();
        }

        
    }
}
