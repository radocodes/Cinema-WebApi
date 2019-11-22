using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Projection;
using System.Threading.Tasks;

namespace CinemAPI.Domain
{
    public class NewProjectionCreation : INewProjection
    {
        private readonly IProjectionRepository projectionsRepo;
        private readonly IRoomRepository roomRepo;

        public NewProjectionCreation(IProjectionRepository projectionsRepo, IRoomRepository roomRepo)
        {
            this.projectionsRepo = projectionsRepo;
            this.roomRepo = roomRepo;
        }

        public async Task<NewProjectionSummary> NewAsync(IProjectionCreation projection)
        {
            var currRoom = await roomRepo.GetByIdAsync(projection.RoomId);
            var availableSeatsCount = currRoom.Rows * currRoom.SeatsPerRow;

             await projectionsRepo.InsertAsync(new Projection(projection.MovieId, projection.RoomId, projection.StartDate, availableSeatsCount));

            return new NewProjectionSummary(true);
        }
    }
}