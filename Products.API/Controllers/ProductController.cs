using Products.API.Mapping;
using Products.API.Models;
using Products.Domain;
using Products.Domain.Command;
using Products.Domain.Data.Repositories;
using Products.Domain.Entities;
using Products.Domain.Handlers;
using Products.Domain.Handlers.Products;
using Products.Domain.Validation.Products;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace Products.API.Controllers
{
    /// <summary>
    /// API de Produtos
    /// </summary>
    [RoutePrefix("api/v1/products")]
    public class ProductController : BaseController
    {
        private readonly ProductHandler productHandler;
        private readonly IRepository<Product> productRepository;

        public ProductController(ProductHandler productHandler, IRepository<Product> productRepository)
        {
            this.productHandler = productHandler;
            this.productRepository = productRepository;
        }

        /// <summary>
        /// Retorna todos os produtos
        /// </summary>
        /// <remarks>
        /// Retorna todos os produtos
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            var product = productRepository.GetAll(new Pagination(), null, null, "Category");
            return Ok(product.ToModel());
        }

        /// <summary>
        /// Retorna o produto por Id
        /// </summary>
        /// <remarks>
        /// <paramref name="id"/>
        /// Retorna o produto por Id
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            var product = productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product.ToModel());
        }

        /// <summary>
        /// Insere o produto
        /// </summary>
        /// <remarks>
        /// Insere o produto
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="500"></response>
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody]CreateProductCommand command)
        {
            var result = (CommandResult)productHandler.Handle(command);
            if (!result.Success)
            {
                InternalServerCustom(result);
            }

            return Ok(result.Message);
        }

        /// <summary>
        /// Atualiza o produto
        /// </summary>
        /// <remarks>
        /// Atualiza o produto
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="500"></response>
        [HttpPut]
        [Route("")]
        public IHttpActionResult Put([FromBody]UpdateProductCommand command)
        {
            var result = (CommandResult)productHandler.Handle(command);
            if (!result.Success)
            {
                InternalServerCustom(result);
            }

            return Ok(result.Message);
        }

        /// <summary>
        /// Deleta o produto por Id
        /// </summary>
        /// <remarks>
        /// <paramref name="id"/>
        /// Deleta o produto por Id
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Mensagem de sucesso</response>
        /// <response code="500">Mensagem de sucesso</response>
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete([FromUri]int id)
        {
            var result = (CommandResult)productHandler.Handle(new DeleteProductCommand(id));
            if (!result.Success)
            {
                InternalServerCustom(result);
            }

            return Ok(result.Message);
        }
    }
}
