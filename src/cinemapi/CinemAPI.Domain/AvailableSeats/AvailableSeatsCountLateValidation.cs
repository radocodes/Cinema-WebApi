using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Projection;
using System;
using System.Threading.Tasks;

namespace CinemAPI.Domain.AvailableSeats
{
    public class AvailableSeatsCountLateValidation : IAvailableSeatsCount
    {
        private readonly IProjectionRepository projectionsRepo;
        private readonly IAvailableSeatsCount availableSeats;

        public AvailableSeatsCountLateValidation(IAvailableSeatsCount availableSeats,
            IProjectionRepository projectionsRepo)
        {
            this.projectionsRepo = projectionsRepo;
            this.availableSeats = availableSeats;
        }

        public async Task<AvailableSeatsSummary> GetCountAsync(IProjectionIdentifier projIdentifier)
        {
            var currProjection = await projectionsRepo.GetProjectionByIdAsync(projIdentifier.ProjectionId);

            if (currProjection.StartDate <= DateTime.Now)
            {
                var constraintMessage = "You are checking for seats too late for this projection! Seats are no longer available.";

                return new AvailableSeatsSummary(false, constraintMessage);
            }
            else
            {
                return await availableSeats.GetCountAsync(projIdentifier);
            }
        }
    }
}
