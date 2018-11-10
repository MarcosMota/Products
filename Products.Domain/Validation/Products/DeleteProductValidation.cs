using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Domain.Validation.Products
{
    public class DeleteProductValidation : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductValidation ValidateProduct()
        {
            RuleFor(c => c.ProductId)
                .NotEqual(0)
                .NotNull()
                .WithMessage("Por favor informe o id do produto.");


            return this;
        }
    }
}
