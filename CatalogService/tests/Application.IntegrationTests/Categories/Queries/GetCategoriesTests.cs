using CatalogService.Application.Categories.Queries.GetCategories;

using CatalogService.Domain.Entities;

using FluentAssertions;
using NUnit.Framework;

namespace CatalogService.Application.IntegrationTests.Categories.Queries;

using static Testing;

public class GetCategoriesTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnAllCategories()
    {
        await RunAsDefaultUserAsync();

        await AddAsync(new Category
        {
            Name = "test",
            Image = "test.url"
        });

        var query = new GetCategoriesQuery();

        var result = await SendAsync(query);

        result.Should().NotBeEmpty();
    }
}
