using Movie.Options;
using Movie.Services;

namespace Movie.Extensions
{
    //Extenxion ->
    //1) class static
    //2) method static
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMovieService(this IServiceCollection services, Action<MovieApiOptions> options)
        {
           services.AddScoped<IMovieApiServices, MovieApiServices>();
            services.Configure(options);
            return services;
        }
    }
}
