using FluentValidation.Results;
using Products.Domain.Command;
using Products.Domain.Data.Repositories;
using Products.Domain.Entities;
using Products.Domain.Validation.Products;
using System.Collections.Generic;
using System.Linq;

namespace Products.Domain.Handlers.Products
{
    public class ProductHandler :
        ICommandHandler<CreateProductCommand>,
        ICommandHandler<UpdateProductCommand>,
        ICommandHandler<DeleteProductCommand>
    {
        private IRepository<Product> productRepository;
        private IRepository<Category> categoryRepository;

        public ProductHandler(IRepository<Product> productRepository, IRepository<Category> categoryRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
        }



        /// <summary>
        /// Handler de Criação de produto
        /// </summary>
        /// <param name="command"></param>
        /// <returns>result - ICommandResult</returns>
        public ICommandResult Handle(CreateProductCommand command)
        {
            if (command.IsValid)
            {
                // Verifica se categoria já foi cadastrada
                var category = categoryRepository.GetById(command.CategoryId);
                if (category == null)
                {
                    return new CommandResult(false, "Categoria informada não existe.", command.Errors);
                }

                // Verifica se existe um produto com mesmo nome
                List<Product> ProductsWithTheSameName = productRepository.GetAll(null, p => p.Name == command.Name, null).ToList();
                if (ProductsWithTheSameName.Count() > 0)
                {
                    return new CommandResult(false, "Já existe produto cadastrado com esse nome.", command.Errors);
                }

                // Cria produto
                var product = new Product()
                {
                    Id = 0,
                    Name = command.Name,
                    Category = category,
                    Price = command.Price,
                };

                // Persiste
                productRepository.Insert(product);
                return new CommandResult(true, "Produto salvo com sucesso.", product);
            }

            return new CommandResult(false, "Problemas em cadastrar produto.", command.Errors);
        }

        /// <summary>
        /// Handler de Atualização de produto
        /// </summary>
        /// <param name="command"></param>
        /// <returns>result - ICommandResult</returns>
        public ICommandResult Handle(UpdateProductCommand command)
        {
            if (command.IsValid)
            {
                var product = productRepository.GetById(command.ProductId);
                if (product == null)
                {
                    return new CommandResult(false, "Produto não foi encontrado", command.Errors);
                }
                // Verifica se categoria já foi cadastrada
                var category = categoryRepository.GetById(command.CategoryId);
                if (category == null)
                {
                    return new CommandResult(false, "Categoria informada não existe.", command.Errors);
                }

                // Verifica se existe um produto com mesmo nome
                List<Product> ProductsWithTheSameName = productRepository.GetAll(null, p => p.Name == command.Name && p.Id != command.ProductId, null).ToList();
                if (ProductsWithTheSameName.Count() > 0)
                {
                    return new CommandResult(false, "Já existe produto cadastrado com esse nome.", command.Errors);
                }

                product.Name = command.Name;
                product.Price = command.Price;
                product.Category = category;
                productRepository.Update(product);
                return new CommandResult(true, "Produto salvo com sucesso.", product);
            }
            else
            {
                return new CommandResult(false, "Problemas ao inserir o produto.", command.Errors);
            }
        }

        /// <summary>
        /// Handler de Exclusão de produto
        /// </summary>
        /// <param name="command"></param>
        /// <returns>result - ICommandResult</returns>
        public ICommandResult Handle(DeleteProductCommand command)
        {
            if (command.IsValid)
            {
                var product = productRepository.GetById(command.ProductId);
                if (product == null)
                {
                    return new CommandResult(false, "Produto não foi encontrado", command.Errors);
                }

                productRepository.Delete(product);
                return new CommandResult(true, "Produto excluido com sucesso.", product);
            }
            else
            {
                return new CommandResult(false, "Problemas ao excluir o produto.", command.Errors);
            }
        }
    }
}
