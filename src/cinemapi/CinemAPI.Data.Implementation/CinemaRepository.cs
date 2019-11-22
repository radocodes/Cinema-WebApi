using CinemAPI.Data.EF;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Cinema;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CinemAPI.Data.Implementation
{
    public class CinemaRepository : ICinemaRepository
    {
        private readonly CinemaDbContext db;

        public CinemaRepository(CinemaDbContext db)
        {
            this.db = db;
        }

        public async Task<ICinema> GetByNameAndAddressAsync(string name, string address)
        {
            return await db.Cinemas.Where(x => x.Name == name &&
                                         x.Address == address)
                             .FirstOrDefaultAsync();
        }

        public async Task InsertAsync(ICinemaCreation cinema)
        {
            Cinema newCinema = new Cinema(cinema.Name, cinema.Address);

            db.Cinemas.Add(newCinema);

            await db.SaveChangesAsync();
        }

        public async Task<List<ICinema>> AllCinemasAsync()
        {
            return await db.Cinemas.ToListAsync<ICinema>();
        }

        public async Task<ICinema> GetByIdAsync(int id)
        {
            return await db.Cinemas.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}