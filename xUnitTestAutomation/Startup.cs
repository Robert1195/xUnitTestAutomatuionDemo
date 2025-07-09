using EaFramework.Config;
using Microsoft.Extensions.DependencyInjection;
using xUnitTestAutomation.Pages;
using EaFramework.Reporting;

namespace xUnitTestAutomation
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton(ConfigReader.ReadConfig())
                .AddSingleton<IReportManager, ReportManager>()
                .AddSingleton<SharedReportFixture>()
                .AddScoped<IDriverFixture, DriverFixture>()
                .AddScoped<IDriverWait, DriverWait>()
                .AddScoped<IHomePage, HomePage>()
                .AddScoped<IProductsPage, ProductsPage>();
        }
    }
}
