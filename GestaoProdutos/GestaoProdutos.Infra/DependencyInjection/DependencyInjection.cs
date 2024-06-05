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
            //serviceCollection.AddScoped<IPersonService, PersonService>();
        }

        private static void RegisterRepositories(IServiceCollection serviceCollection)
        {
            //serviceCollection.AddScoped<IPersonRepository, PersonRepository>();
        }
    }
}
