using AutoMapper;
using CatalogService.Application.Common.Mappings;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Categories.Queries.GetCategories;
public class CategoryDto : IMapFrom<Category>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public int? ParentCategoryId { get; set; }
    public CategoryDto ParentCategory { get; set; }
}
