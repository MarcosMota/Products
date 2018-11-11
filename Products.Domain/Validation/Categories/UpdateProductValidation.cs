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
    public class UpdateCategoryValidation : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryValidation ValidateName()
        {
            RuleFor(c => c.Name).MaximumLength(50)
                            .WithMessage("Nome do produto tem um limite de 50 caracteres.");
            return this;

        }

        public UpdateCategoryValidation ValidateCategory()
        {
            RuleFor(c => c.CategoryId)
                .NotEqual(0)
                .NotNull()
                .WithMessage("Categoria não pode ser nula ou vazia.");
            return this;
        }


    }
}
