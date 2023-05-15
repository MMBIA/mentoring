using CatalogService.Application.Categories.Commands.CreateCategory;
using CatalogService.Application.Categories.Queries.GetCategories;
using CatalogService.Application.Common.Exceptions;

using CatalogService.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace CatalogService.Application.IntegrationTests.Categories.Commands;

using static Testing;

public class CreateCategoryTests : BaseTestFixture
{
    [Test]
    public async Task ShouldCreateCategory()
    {
        var userId = await RunAsDefaultUserAsync();

        var command = new CreateCategoryCommand
        {
            Name = "test",
            Image = "test.url",
            ParentCategoryId = null
        };

        var id = await SendAsync(command);

        var query = new GetCategoriesQuery();

        var result = await SendAsync(query);

        result.Should().NotBeNull();
        result.Should().NotBeEmpty();
        result.Count.Should().Be(1);
        result[0].Id.Should().Be(id);
    }
}
