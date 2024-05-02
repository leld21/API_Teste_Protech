using Microsoft.Extensions.DependencyInjection;
using ProtechAnime.Infrastructure.Persistence;

namespace ProtechAnime.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<AnimeDbContext>();
        }
    }
}
