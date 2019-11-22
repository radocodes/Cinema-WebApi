using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Projection;
using System.Threading.Tasks;

namespace CinemAPI.Domain.AvailableSeats
{
    public class AvailableSeatsCountExistValidation : IAvailableSeatsCount
    {
        private readonly IProjectionRepository projectionsRepo;
        private readonly IAvailableSeatsCount availableSeats;

        public AvailableSeatsCountExistValidation(IProjectionRepository projectionsRepo,
            IAvailableSeatsCount availableSeats)
        {
            this.projectionsRepo = projectionsRepo;
            this.availableSeats = availableSeats;
        }

        public async Task<AvailableSeatsSummary> GetCountAsync(IProjectionIdentifier projIdentifier)
        {
            var currProjection = await projectionsRepo.GetProjectionByIdAsync(projIdentifier.ProjectionId);
            
            if (currProjection == null)
            {
                string constraintMessage = $"Projection with Id: {projIdentifier.ProjectionId}, not exist!";

                return new AvailableSeatsSummary(false, constraintMessage);
            }
            else
            {
                return await availableSeats.GetCountAsync(projIdentifier);
            }
        }
    }
}
