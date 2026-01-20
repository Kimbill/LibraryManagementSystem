using LawPavillion.LibraryManagement.Infrastructure.Db;
using Microsoft.EntityFrameworkCore;

namespace LawPavillion.LibraryManagement.Api.Extensions
{
    public static class DbExtension
    {
        public static void AddDatabase(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    config.GetConnectionString("DefaultConnection"),
                    sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                    }));
        }
    }
}
