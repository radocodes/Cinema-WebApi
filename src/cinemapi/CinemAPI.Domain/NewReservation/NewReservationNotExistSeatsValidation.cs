using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Reservation;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewReservation
{
    public class NewReservationNotExistSeatsValidation : INewReservation
    {
        private readonly INewReservation newReservation;
        private readonly IProjectionRepository projectionRepo;
        private readonly IRoomRepository roomRepo;

        public NewReservationNotExistSeatsValidation(
            INewReservation newReservation, IRoomRepository roomRepo, IProjectionRepository projectionRepo)
        {
            this.newReservation = newReservation;
            this.projectionRepo = projectionRepo;
            this.roomRepo = roomRepo;
        }


        public async Task<NewReservationSummary> NewAsync(IReservationCreation reservation)
        {
            var currProjection = await projectionRepo.GetProjectionByIdAsync(reservation.ProjectionId);
            var currRoom = await roomRepo.GetByIdAsync(currProjection.RoomId);

            if ((currRoom.Rows < reservation.Row) || (currRoom.SeatsPerRow < reservation.Column))
            {
                var constraintMessage = "This seat not exist in this room!";

                return new NewReservationSummary(false, constraintMessage);
            }
            else
            {
                return await newReservation.NewAsync(reservation);
            }
        }
    }
}
