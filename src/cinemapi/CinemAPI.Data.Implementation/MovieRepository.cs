using CinemAPI.Data.EF;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Movie;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CinemAPI.Data.Implementation
{
    public class MovieRepository : IMovieRepository
    {
        private readonly CinemaDbContext db;

        public MovieRepository(CinemaDbContext db)
        {
            this.db = db;
        }

        public async Task<List<IMovie>> GetAllMoviesAsync()
        {
            return await db.Movies.ToListAsync<IMovie>();
        }

        public async Task<IMovie> GetByIdAsync(int movieId)
        {
            return await db.Movies.FirstOrDefaultAsync(x => x.Id == movieId);
        }

        public async Task<IMovie> GetByNameAndDurationAsync(string name, short duration)
        {
            return await db.Movies.FirstOrDefaultAsync(x => x.Name == name &&
                                                 x.DurationMinutes == duration);
        }

        public async Task InsertAsync(IMovieCreation movie)
        {
            Movie newMovie = new Movie(movie.Name, movie.DurationMinutes);

            db.Movies.Add(newMovie);
            await db.SaveChangesAsync();
        }
    }
}