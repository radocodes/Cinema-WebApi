using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Reservation;
using System;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewReservedTicket
{
    public class NewReservedTicketLateValidation : INewReservedTicket
    {
        private readonly IProjectionRepository projectionRepo;
        private readonly IReservationRepository reservationRepo;
        private readonly INewReservedTicket newReservedTicket;

        public NewReservedTicketLateValidation(
            IProjectionRepository projectionRepo,
            IReservationRepository reservationRepo,
            INewReservedTicket newReservedTicket)
        {
            this.projectionRepo = projectionRepo;
            this.reservationRepo = reservationRepo;
            this.newReservedTicket = newReservedTicket;
        }

        public async Task<NewReservedTicketSummary> NewAsync(IReservationIdentifier reservationIdentifier)
        {
            var reservation = await reservationRepo.GetByIdAsync(reservationIdentifier.ReservationId);
            var currProjection = await projectionRepo.GetProjectionByIdAsync(reservation.ProjectionId);

            var endingTimeToTakeReservedTicket = currProjection.StartDate - TimeSpan.FromSeconds(10);

            if (endingTimeToTakeReservedTicket <= DateTime.Now)
            {
                var constraintMessage = "It is too late to buy tickets with reservation for this projection! You can try to buy ticket without reservation.";

                return new NewReservedTicketSummary(false, constraintMessage);
            }
            else
            {
                return await newReservedTicket.NewAsync(reservationIdentifier);
            }
        }
    }
}
