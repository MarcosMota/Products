using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Products.API.Models
{
    public class ProductModel
    {
        public ProductModel()
        {
        }

        public ProductModel(int productId, string name, decimal price, CategoryModel category)
        {
            ProductId = productId;
            Name = name;
            Price = price;
            Category = category;
        }

        public int ProductId { get;  }
        public string Name { get;  }
        public decimal Price { get; }
        public CategoryModel Category { get; }

    }
}