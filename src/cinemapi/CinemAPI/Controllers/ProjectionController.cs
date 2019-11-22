using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models;
using CinemAPI.Models.Input.Projection;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace CinemAPI.Controllers
{
    public class ProjectionController : ApiController
    {
        private readonly INewProjection newProj;
        private readonly IAvailableSeatsCount checkingAvailableSeats;
        private readonly IProjectionRepository projectionRepo;
        private readonly ICancelReservations cancelReservations;

        public ProjectionController(
            INewProjection newProj,
            IAvailableSeatsCount checkingAvailableSeats,
            IProjectionRepository projectionRepo,
            ICancelReservations cancelReservations)
        {
            this.newProj = newProj;
            this.checkingAvailableSeats = checkingAvailableSeats;
            this.projectionRepo = projectionRepo;
            this.cancelReservations = cancelReservations;
        }

        [HttpGet]
        public async Task<IHttpActionResult> AvailableSeats(AvailableSeatsModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await cancelReservations.CancelExpiredReservationsAsync(DateTime.Now);

            AvailableSeatsSummary summary = await checkingAvailableSeats
                .GetCountAsync(new ProjectionIdentifier(model.ProjectionId));
            
            if (summary.IsSuccessfull)
            {
                return Ok(summary.AvailableSeats);
            }
            else
            {
                return BadRequest(summary.Message);
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateProjection(ProjectionCreationModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            NewProjectionSummary summary = await newProj
                .NewAsync(new Projection(model.MovieId, model.RoomId, model.StartDate));

            if (summary.IsCreated)
            {
                return Ok();
            }
            else
            {
                return BadRequest(summary.Message);
            }
        }
    }
}