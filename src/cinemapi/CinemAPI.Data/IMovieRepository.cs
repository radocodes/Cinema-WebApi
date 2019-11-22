using CinemAPI.Models.Contracts.Movie;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemAPI.Data
{
    public interface IMovieRepository
    {
        Task<List<IMovie>> GetAllMoviesAsync();

        Task<IMovie> GetByIdAsync(int movieId);

        Task<IMovie> GetByNameAndDurationAsync(string name, short duration);

        Task InsertAsync(IMovieCreation movie);
    }
}