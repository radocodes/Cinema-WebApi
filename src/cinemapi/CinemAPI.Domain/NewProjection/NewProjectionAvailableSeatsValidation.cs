using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Projection;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewProjection
{
    public class NewProjectionAvailableSeatsValidation : INewProjection
    {
        private readonly INewProjection newProj;

        public NewProjectionAvailableSeatsValidation(INewProjection newProj)
        {
            this.newProj = newProj;
        }

        public async Task<NewProjectionSummary> NewAsync(IProjectionCreation projection)
        {
            if (projection.AvailableSeatsCount < 0)
            {
                var constraintMessage = "AvailableSeatsCount can not accept negative values.";
                return new NewProjectionSummary(false, constraintMessage);
            }

            return await newProj.NewAsync(projection);
        }
    }
}
