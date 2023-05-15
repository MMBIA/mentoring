using CatalogService.Application.Categories.Commands.CreateCategory;
using CatalogService.Application.Categories.Commands.DeleteCategory;
using CatalogService.Application.Categories.Queries.GetCategories;
using CatalogService.Application.Common.Exceptions;

using CatalogService.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace CatalogService.Application.IntegrationTests.Categories.Commands;

using static Testing;

public class DeleteCategoryTests : BaseTestFixture
{
    [Test]
    public async Task ShouldDeleteCategory()
    {
        await RunAsDefaultUserAsync();

        var command = new CreateCategoryCommand
        {
            Name = "test",
            Image = "test.url",
            ParentCategoryId = null
        };

        var id = await SendAsync(command);

        await SendAsync(new DeleteCategoryCommand(id));

        var query = new GetCategoriesQuery();

        var result = await SendAsync(query);

        result.Should().NotBeNull();
        result.Should().BeEmpty();
        result.Count.Should().Be(0);
    }
}
