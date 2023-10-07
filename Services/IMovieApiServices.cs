using Movie.Models;

namespace Movie.Services
{
    public interface IMovieApiServices
    {
        Task<MovieApiResponse> SearchByTitleAsync(string title);
        Task<Cinema> SearchByIdAsync(string id);
    }
}