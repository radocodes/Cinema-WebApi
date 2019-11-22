using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Reservation;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewReservation
{
    public class NewReservationUniqueValidation : INewReservation
    {
        private readonly INewReservation newReservation;
        private readonly IReservationRepository reservationRepo;
        private readonly ITicketRepository ticketRepo;


        public NewReservationUniqueValidation(
            INewReservation newReservation,
            IReservationRepository reservationRepo,
            ITicketRepository ticketRepo)
        {
            this.newReservation = newReservation;
            this.reservationRepo = reservationRepo;
            this.ticketRepo = ticketRepo;
        }

        public async Task<NewReservationSummary> NewAsync(IReservationCreation reservation)
        {
            var reservationFromAnother = await reservationRepo
                .GetAsync(reservation.ProjectionId, reservation.Row, reservation.Column);

            var TicketFromAnother = await ticketRepo
                .GetAsync(reservation.ProjectionId, reservation.Row, reservation.Column);

            if (reservationFromAnother != null || TicketFromAnother != null)
            {
                var constraintMessage = "This seat is already reserved!";

                return new NewReservationSummary(false, constraintMessage);
            }
            else
            {
                return await newReservation.NewAsync(reservation);
            }
        }
    }
}
