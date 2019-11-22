using CinemAPI.Models.Contracts.Projection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemAPI.Data
{
    public interface IProjectionRepository
    {
        Task<List<IProjection>> GetAllProjectionsAsync();

        Task<IProjection> GetAsync(int movieId, int roomId, DateTime startDate);

        Task InsertAsync(IProjectionCreation projection);

        Task<IProjection> GetProjectionByIdAsync(long projectionId);

        Task<List<IProjection>> GetActiveProjectionsAsync(int roomId);

        Task DecreaseAvailableSeatsAsync(long projectionId);

        Task IncreaseAvailableSeatsAsync(long projectionId);

        Task RemoveAsync(long projectionId);
    }
}