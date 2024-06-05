using GestaoDominio.Application.Services;
using GestaoProdutos.Domain.Interfaces.Repositories;
using GestaoProdutos.Domain.Interfaces.Services;
using GestaoProdutos.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GestaoProdutos.Infra.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void Register(IServiceCollection serviceCollection)
        {
            RegisterServices(serviceCollection);
            RegisterRepositories(serviceCollection);
        }

        private static void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IProductService, ProductService>();
        }

        private static void RegisterRepositories(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}
