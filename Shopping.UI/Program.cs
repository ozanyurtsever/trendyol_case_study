using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Trendyol.Shopping.Business.Cart.Abstract;
using Trendyol.Shopping.Business.Cart.Concrete;
using Trendyol.Shopping.Business.Delivery.Abstract;
using Trendyol.Shopping.Business.Delivery.Concrete;

namespace Shopping.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = ConfigureServices();

            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<App>().Run();
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            var config = LoadConfiguration();
            services.AddSingleton(config);

            // required to run the application
            services.AddTransient<ICostCalculator, StandardCalculatorStrategy>();
            services.AddTransient<IShoppingCart, ShoppingCart>();
            services.AddTransient<App>();

            return services;
        }

        public static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        }
    }
}
