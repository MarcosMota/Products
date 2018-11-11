using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Domain.Validation.Products
{
    public class DeleteCategoryValidation : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryValidation ValidateCategory()
        {
            RuleFor(c => c.CategoryId)
                .NotEqual(0)
                .NotNull()
                .WithMessage("Por favor informe o id da categoria.");


            return this;
        }
    }
}
