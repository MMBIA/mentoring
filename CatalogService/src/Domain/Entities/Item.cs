﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CatalogService.Domain.Entities;

//public class Item : BaseAuditableEntity
//{
//    
//    public string Name { get; set; }
//    public string Description { get; set; }
//    public string Image { get; set; }
//    public int CategoryId { get; set; }
//    public decimal Price { get; set; }
//    public int Amount { get; set; }
//}

public class Item : BaseAuditableEntity
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string Image { get; set; }

    public int CategoryId { get; set; }

    public Category Category { get; set; }

    public double Price { get; set; }

    public int Amount { get; set; }
}