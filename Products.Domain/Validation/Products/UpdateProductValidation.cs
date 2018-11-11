using FluentValidation;
using Products.Domain.Command;
using Products.Domain.Data.Repositories;
using Products.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Domain.Validation.Products
{
    public class UpdateProductValidation : AbstractValidator<UpdateProductCommand>
    {

        public UpdateProductValidation ValidatePrice()
        {
            RuleFor(c => c.Price).NotNull()
                            .WithMessage("Preço não pode ser nulo.");
            return this;
        }

        public UpdateProductValidation ValidateName()
        {
            RuleFor(c => c.Name).MaximumLength(50)
                            .WithMessage("Nome do produto tem um limite de 50 caracteres.");
            return this;

        }

        public UpdateProductValidation ValidateCategory()
        {
            RuleFor(c => c.CategoryId)
                .NotEqual(0)
                .NotNull()
                .WithMessage("Categoria não pode ser nula ou vazia.");
            return this;
        }

        public UpdateProductValidation ValidateProduct()
        {
            RuleFor(c => c.ProductId)
                .NotEqual(0)
                .NotNull()
                .WithMessage("Por favor informe o id do produto.");

       

            return this;
        }

       
    }
}
