using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Projection;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewProjection
{
    public class NewProjectionUniqueValidation : INewProjection
    {
        private readonly IProjectionRepository projectRepo;
        private readonly INewProjection newProj;

        public NewProjectionUniqueValidation(IProjectionRepository projectRepo, INewProjection newProj)
        {
            this.projectRepo = projectRepo;
            this.newProj = newProj;
        }

        public async Task<NewProjectionSummary> NewAsync(IProjectionCreation proj)
        {
            IProjection projection = await projectRepo.GetAsync(proj.MovieId, proj.RoomId, proj.StartDate);

            if (projection != null)
            {
                return new NewProjectionSummary(false, "Projection already exists");
            }

            return await newProj.NewAsync(proj);
        }
    }
}