using Products.API.Models;
using Products.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Products.API.Mapping
{
    public static class ModelMapping
    {
        public static IEnumerable<ProductModel> ToModel(this IEnumerable<Product> products)
        {
            if (products == null)
                return new List<ProductModel>();

            return products.Select(c => new ProductModel(c.Id, c.Name, c.Price, c.Category.ToModel()));
        }

        public static ProductModel ToModel(this Product product)
        {
            if (product == null)
                return new ProductModel();

            return new ProductModel(product.Id, product.Name, product.Price, product.Category.ToModel());
        }

        public static CategoryModel ToModel(this Category category)
        {
            if (category == null)
                return new CategoryModel();

            return new CategoryModel()
            {
                CategoryId = category.Id,
                Name = category.Name,
            };
        }

        public static IEnumerable<CategoryModel> ToModel(this IEnumerable<Category> products)
        {
            if (products == null)
                return new List<CategoryModel>();

            return products.Select(c => new CategoryModel(c.Id, c.Name));
        }
    }
}