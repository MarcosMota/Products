using FluentValidation.Results;
using Products.Domain.Handlers;
using Products.Domain.Validation.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Domain.Command
{
    public class UpdateProductCommand : ICommand
    {
        public UpdateProductCommand(int productId,int categoryId, string name, decimal price)
        {
            CategoryId = categoryId;
            Name = name;
            Price = price;
            ProductId = productId;

            // Validação do comando
            var validator = new UpdateProductValidation()
                .ValidateName()
                .ValidateProduct()
                .ValidateCategory()
                .ValidatePrice()
                .Validate(this);

            IsValid = validator.IsValid;
            Errors = validator.Errors;
        }

        public int CategoryId { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public bool IsValid { get; }
        public IList<ValidationFailure> Errors { get; }
        public int ProductId { get; private set; }
    }
}
