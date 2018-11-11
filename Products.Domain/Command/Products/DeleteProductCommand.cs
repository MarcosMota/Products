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
    public class DeleteProductCommand : ICommand
    {
        private readonly DeleteProductValidation validator = null;

        public DeleteProductCommand(int productId)
        {
            ProductId = productId;
            validator = new DeleteProductValidation();
        }
        public int ProductId { get; private set; }
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
