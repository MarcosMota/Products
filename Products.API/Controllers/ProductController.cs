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

        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            var product = productRepository.GetAll(new Pagination(), null, null, "Category");
            return Ok(product.ToModel());
        }

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
