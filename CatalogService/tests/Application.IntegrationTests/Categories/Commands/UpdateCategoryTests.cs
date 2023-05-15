using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatalogService.Application.Categories.Commands.CreateCategory;
using CatalogService.Application.Categories.Commands.UpdateCategory;

using CatalogService.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace CatalogService.Application.IntegrationTests.Categories.Commands;

using static Testing;

public class UpdateCategoryTests : BaseTestFixture
{
    [Test]
    public async Task ShouldUpdateTodoList()
    {
        var userId = await RunAsDefaultUserAsync();

        var categoryId = await SendAsync(new CreateCategoryCommand
        {
            Name = "test",
            Image = "test.url",
            ParentCategoryId = null
        });


        var command = new UpdateCategoryCommand
        {
            Id = categoryId,
            Name = "test2",
            Image = "test.url",
            ParentCategoryId = null
        };

        await SendAsync(command);

        var category = await FindAsync<Category>(categoryId);

        category.Should().NotBeNull();
        category!.Name.Should().Be("test2");
        category.LastModifiedBy.Should().NotBeNull();
        category.LastModifiedBy.Should().Be(userId);
        category.LastModified.Should().NotBeNull();
        category.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
