using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Ticket;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewTicket
{
    public class NewTicketSeatValidation : INewTicket
    {
        private readonly ITicketRepository ticketRepo;
        private readonly IReservationRepository reservationRepo;
        private readonly INewTicket newTicket;

        public NewTicketSeatValidation(
            ITicketRepository ticketRepo, 
            IReservationRepository reservationRepo,
            INewTicket newTicket)
        {
            this.ticketRepo = ticketRepo;
            this.reservationRepo = reservationRepo;
            this.newTicket = newTicket;
        }

        public async Task<NewTicketSummary> NewAsync(ITicketCreation ticket)
        {
            var existinglReservation = await reservationRepo
                .GetAsync(ticket.ProjectionId, ticket.Row, ticket.Column);

            var existingTicket = await ticketRepo.GetAsync(ticket.ProjectionId, ticket.Row, ticket.Column);

            if (existinglReservation != null || existingTicket != null)
            {
                var constraintMessage = "This seat is not available! You can not buy ticket for it";

                return new NewTicketSummary(false, constraintMessage);
            }
            else
            {
                return await newTicket.NewAsync(ticket);
            }
        }
    }
}
