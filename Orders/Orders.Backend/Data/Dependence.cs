using Microsoft.EntityFrameworkCore;
using Orders.Backend.Intertfaces;
using Orders.Backend.Repositories;
using Orders.Backend.Services;
using Orders.Backend.UnitsOfWork;

namespace Orders.Backend.Data
{
    public static class Dependence
    {
        public static void InjectDependencies(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<DataContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("DockerConnection"));
            });

            services.AddScoped(typeof(IGenericUnitOfWork<>), typeof(GenericUnitOfWork<>));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IApiService, ApiService>();
            services.AddTransient<SeedDb>();

            //services.AddScoped<IFileStorage, FileStorage>();
            //services.AddScoped<IMailHelper, MailHelper>();
            //services.AddScoped<IOrdersHelper, OrdersHelper>();
        }
    }
}
