using Products.Domain.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Products.API.Controllers
{
    public class BaseController: ApiController
    {
        public void InternalServerCustom(CommandResult result)
        {
            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            response.Content = new StringContent(result.Message);
            throw new HttpResponseException(response);
        }
    }
}