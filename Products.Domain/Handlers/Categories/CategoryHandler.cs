using FluentValidation.Results;
using Products.Domain.Command;
using Products.Domain.Data.Repositories;
using Products.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Domain.Handlers.Categories
{
    public class CategoryHandler :
        ICommandHandler<CreateCategoryCommand>,
        ICommandHandler<UpdateCategoryCommand>,
        ICommandHandler<DeleteCategoryCommand>
    {
        private IRepository<Category> categoryRepository;
        private IRepository<Product> productRepository;

        public CategoryHandler(IRepository<Category> categoryRepository, IRepository<Product> productRepository)
        {
            this.categoryRepository = categoryRepository;
            this.productRepository = productRepository;
        }

        public ICommandResult Handle(CreateCategoryCommand command)
        {
            if (command.IsValid)
            {
                
                var category = new Category()
                {
                    Id = 0,
                    Name = command.Name
                };

                categoryRepository.Insert(category);
                return new CommandResult(true, "Categoria salva com sucesso.", category);
            }

            return new CommandResult(false, "Problemas em cadastrar categoria.", command.Errors);
        }

        public ICommandResult Handle(UpdateCategoryCommand command)
        {
            if (command.IsValid)
            {
                var category = categoryRepository.GetById(command.CategoryId);
                if (category == null)
                {
                    command.Errors.Add(new ValidationFailure("CategoryId", "Categoria não foi encontrado"));
                    return new CommandResult(false, "Problemas ao atulizar o categoria.", command.Errors);
                }
                category.Name = command.Name;
                categoryRepository.Update(category);
                return new CommandResult(true, "Produto salvo com sucesso.", category);
            }
            else
            {
                return new CommandResult(false, "Problemas ao atulizar o categoria.", command.Errors);
            }
        }

        public ICommandResult Handle(DeleteCategoryCommand command)
        {
            if (command.IsValid)
            {
                var category = categoryRepository
                    .Table
                    .Include(p=>p.Products)
                    .FirstOrDefault(p=>p.Id == command.CategoryId);
                if (category == null)
                {
                    return new CommandResult(false, "Categoria não foi encontrado", command.Errors);
                }

               
                categoryRepository.Delete(category);
                return new CommandResult(true, "Categoria excluido com sucesso.", category);
            }

            return new CommandResult(false, "Problemas ao atulizar o categoria.", command.Errors);

        }
    }
}
