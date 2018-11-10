using FluentValidation.Results;
using Products.Domain.Handlers;
using Products.Domain.Validation.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Domain
{
    public class CreateProductCommand : ICommand
    {
        private readonly CreateProductValidation validator = null;
        public CreateProductCommand(string name, int categoryId, decimal price)
        {
            Name = name;
            CategoryId = categoryId;
            Price = price;
            validator = new CreateProductValidation();
        }

        public string Name { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public bool IsValid
        {
            get
            {
                var result = validator.Validate(this);
                return result.IsValid;
            }
        }
        public IList<ValidationFailure> Errors
        {
            get
            {
                var result = validator.Validate(this);
                return result.Errors;
            }
        }
    }
}
