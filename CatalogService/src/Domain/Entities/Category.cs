using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CatalogService.Domain.Entities;

//public class Category : BaseAuditableEntity
//{
//    //public int Id { get; set; }
//    public string Name { get; set; }
//    public string Image { get; set; }
//    public int? ParentCategoryId { get; set; }
//}

public class Category : BaseAuditableEntity
{
    public string Name { get; set; }
    public string Image { get; set; }
    public int? ParentCategoryId { get; set; }
    public Category ParentCategory { get; set; }

    //public ICollection<Category> SubCategories { get; set; }

    //public ICollection<Item> Items { get; set; }
}