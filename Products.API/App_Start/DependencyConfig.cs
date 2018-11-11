
using Products.Domain.Data.DbContexts;
using Products.Domain.Data.Repositories;
using Products.Domain.Handlers.Products;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.WebApi;
using System.Web.Http;

namespace Products.API.App_Start
{
    public static class DependencyConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            container.Register<ProductDbContext>(Lifestyle.Scoped);
            container.Register(typeof(IRepository<>), typeof(BaseRepository<>),Lifestyle.Scoped);
            container.Register<ProductHandler>(Lifestyle.Scoped);
            container.RegisterWebApiControllers(config);
            container.Verify();

            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}