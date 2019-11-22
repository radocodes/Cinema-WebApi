using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Projection;
using System.Threading.Tasks;

namespace CinemAPI.Domain.AvailableSeats
{
    public class AvailableSeatsCount : IAvailableSeatsCount
    {
        private readonly IProjectionRepository projectionsRepo;

        public AvailableSeatsCount(IProjectionRepository projectionsRepo)
        {
            this.projectionsRepo = projectionsRepo;
        }

        public async Task<AvailableSeatsSummary> GetCountAsync(IProjectionIdentifier projIdentifier)
        {
            var currProjection = await projectionsRepo.GetProjectionByIdAsync(projIdentifier.ProjectionId);

            var availableSeatsCount = currProjection.AvailableSeatsCount;

            return new AvailableSeatsSummary(true, availableSeatsCount);
        }
    }
}
