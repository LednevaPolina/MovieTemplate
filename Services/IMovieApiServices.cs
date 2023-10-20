using Movie.Models;

namespace Movie.Services
{
    public interface IMovieApiServices
    {
        Task<MovieApiResponse> SearchByTitleAsync(string title, int page=1);
        Task<Cinema> SearchByIdAsync(string id);
    }
}