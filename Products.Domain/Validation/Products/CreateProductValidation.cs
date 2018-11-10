using FluentValidation;
using Products.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Domain.Validation.Products
{
    public class CreateProductValidation : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidation()
        {
            RuleFor(c => c.CategoryId)
                .NotEqual(0)
                .NotNull()
                .WithMessage("Categoria não pode ser nula ou vazia.");

            RuleFor(c => c.Name).MaximumLength(50)
                .WithMessage("Nome do produto tem um limite de 50 caracteres.");

            RuleFor(c => c.Price).NotNull()
                .WithMessage("Preço não pode ser nulo.");
        }
    }
}
