using Products.API.Mapping;
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

namespace Products.API.Controllers
{
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

        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(repository.GetAll(new Pagination(), null, null, "").ToModel());
        }

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
