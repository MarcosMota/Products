using Products.API.Mapping;
using Products.API.Models;
using Products.Domain;
using Products.Domain.Command;
using Products.Domain.Data.Repositories;
using Products.Domain.Entities;
using Products.Domain.Handlers.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Description;

namespace Products.API.Controllers
{
    /// <summary>
    /// API de Categorias
    /// </summary>
    [RoutePrefix("api/v1/category")]
    public class CategoryController : BaseController
    {
        private readonly CategoryHandler categoryHandler;
        private readonly IRepository<Category> repository;

        public CategoryController(CategoryHandler categoryHandler, IRepository<Category> repository)
        {
            this.categoryHandler = categoryHandler;
            this.repository = repository;
        }


        /// <summary>
        /// Retorna todos as categorias
        /// </summary>
        /// <remarks>
        /// Retorna todos as categorias
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>

        [HttpGet]
        [Route("")]
        [ResponseType(typeof(IEnumerable<CategoryModel>))]

        public IHttpActionResult Get()
        {
            return Ok(repository.GetAll(new Pagination(), null, null, "").ToModel());
        }

        /// <summary>
        /// Retorna todos a categoria por id
        /// </summary>
        /// <remarks>
        /// <paramref name="id"/>
        /// Retorna todos a categoria por id
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            var category = repository.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category.ToModel());
        }

        /// <summary>
        /// Insere a categoria
        /// </summary>
        /// <remarks>
        /// <paramref name="command"/>
        /// Insere a categoria
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody]CreateCategoryCommand command)
        {
            var result = (CommandResult)categoryHandler.Handle(command);
            if (!result.Success)
            {
                InternalServerCustom(result);
            }

            return Ok(result.Message);
        }

        /// <summary>
        /// Altera a categoria
        /// </summary>
        /// <remarks>
        /// <paramref name="command"/>
        /// Altera a categoria
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpPut]
        [Route("")]
        public IHttpActionResult Put([FromBody]UpdateCategoryCommand command)
        {
            var result = (CommandResult)categoryHandler.Handle(command);
            if (!result.Success)
            {
                InternalServerCustom(result);
            }

            return Ok(result.Message);
        }

        /// <summary>
        /// Deleta categoria por Id
        /// </summary>
        /// <remarks>
        /// <paramref name="id"/>
        /// Retorna todos os produtos
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete([FromUri]int id)
        {
            var result = (CommandResult)categoryHandler.Handle(new DeleteCategoryCommand(id));
            if (!result.Success)
            {
                InternalServerCustom(result);
            }

            return Ok(result.Message);
        }
    }
}
