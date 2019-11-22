using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Reservation;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewReservedTicket
{
    public class NewReservedTicketNotExistValidation : INewReservedTicket
    {
        private readonly IReservationRepository reservationRepo; 
        private readonly INewReservedTicket newReservedTicket;
        
        public NewReservedTicketNotExistValidation(
            IReservationRepository reservationRepo, 
            INewReservedTicket newReservedTicket)
        {
            this.reservationRepo = reservationRepo;
            this.newReservedTicket = newReservedTicket;
        }

        public async Task<NewReservedTicketSummary> NewAsync(IReservationIdentifier reservationIdentifier)
        {
            var reservation = await reservationRepo.GetByIdAsync(reservationIdentifier.ReservationId);

            if (reservation == null)
            {
                var constraintMessage = "The reservation does not exist.";

                return new NewReservedTicketSummary(false, constraintMessage);
            }
            else
            {
                return await newReservedTicket.NewAsync(reservationIdentifier);
            }
        }
    }
}
