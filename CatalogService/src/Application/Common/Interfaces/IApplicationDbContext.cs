using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Item> Items { get; }
    DbSet<Category> Categories { get; }


    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
