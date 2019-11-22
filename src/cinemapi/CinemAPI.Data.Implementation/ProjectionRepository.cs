using CinemAPI.Data.EF;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Projection;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CinemAPI.Data.Implementation
{
    public class ProjectionRepository : IProjectionRepository
    {
        private readonly CinemaDbContext db;

        public ProjectionRepository(CinemaDbContext db)
        {
            this.db = db;
        }

        public async Task<List<IProjection>> GetAllProjectionsAsync()
        {
            return await db.Projections.ToListAsync<IProjection>();
        }

        public async Task<IProjection> GetProjectionByIdAsync(long projectionId)
        {
            return await db.Projections.FirstOrDefaultAsync(x => x.Id == projectionId);
            
        }

        public async Task<IProjection> GetAsync(int movieId, int roomId, DateTime startDate)
        {
            return await db.Projections.FirstOrDefaultAsync(x => x.MovieId == movieId &&
                                                      x.RoomId == roomId &&
                                                      x.StartDate == startDate);
        }

        public async Task<List<IProjection>> GetActiveProjectionsAsync(int roomId)
        {
            DateTime now = DateTime.UtcNow;

            return await db.Projections.Where(x => x.RoomId == roomId &&
                                             x.StartDate > now).ToListAsync<IProjection>();
        }

        public async Task InsertAsync(IProjectionCreation proj)
        {
            Projection newProj = new Projection(proj.MovieId, proj.RoomId, proj.StartDate, proj.AvailableSeatsCount);

            db.Projections.Add(newProj);
            await db.SaveChangesAsync();
        }

        public async Task DecreaseAvailableSeatsAsync(long projectionId)
        {
            var currProjection = db.Projections.FirstOrDefault(x => x.Id == projectionId);

            currProjection.AvailableSeatsCount -= 1;
            await db.SaveChangesAsync();
        }

        public async Task IncreaseAvailableSeatsAsync(long projectionId)
        {
            var currProjection = db.Projections.FirstOrDefault(x => x.Id == projectionId);

            currProjection.AvailableSeatsCount += 1;
            await db.SaveChangesAsync();
        }

        public async Task RemoveAsync(long projectionId)
        {
            var currProj = db.Projections.FirstOrDefault(x => x.Id == projectionId);

            if (currProj != null)
            {
                db.Projections.Remove(currProj);
                await db.SaveChangesAsync();
            }
        }
    }
}