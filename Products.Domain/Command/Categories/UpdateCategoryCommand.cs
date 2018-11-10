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
    public class UpdateCategoryCommand : ICommand
    {
        public UpdateCategoryCommand(int categoryId, string name)
        {
            CategoryId = categoryId;
            Name = name;

            // Validação do comando
            var validator = new UpdateCategoryValidation()
                .ValidateName()
                .ValidateCategory()
                .Validate(this);

            IsValid = validator.IsValid;
            Errors = validator.Errors;
        }

        public int CategoryId { get; private set; }
        public string Name { get; private set; }
        public bool IsValid { get; }
        public IList<ValidationFailure> Errors { get; }
    }
}
