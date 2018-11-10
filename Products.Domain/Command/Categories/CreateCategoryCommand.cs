using FluentValidation.Results;
using Products.Domain.Handlers;
using Products.Domain.Validation.Categories;
using Products.Domain.Validation.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Domain
{
    public class CreateCategoryCommand : ICommand
    {
        private readonly CreateCategoryValidation validator = null;
        public CreateCategoryCommand(string name)
        {
            Name = name;
            validator = new CreateCategoryValidation();
            validator.ValidateName();
        }
        public string Name { get; set; }
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
