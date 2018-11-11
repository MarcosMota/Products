using FluentValidation.Results;
using System.Collections.Generic;

namespace Products.Domain.Handlers
{
    public interface ICommand
    {
        IList<ValidationFailure> Errors { get; }
        bool IsValid { get; }
    }
}