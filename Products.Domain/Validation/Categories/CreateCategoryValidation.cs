using FluentValidation;
using Products.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Domain.Validation.Categories
{
    public class CreateCategoryValidation : AbstractValidator<CreateCategoryCommand>
    {

        public CreateCategoryValidation ValidateName()
        {
            RuleFor(c => c.Name).MaximumLength(50)
                            .WithMessage("Nome do produto tem um limite de 50 caracteres.");
            return this;
        }
    }
}
