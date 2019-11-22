using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Reservation;
using CinemAPI.Models.Input.ReservedTicket;
using CinemAPI.Models.Input.Ticket;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace CinemAPI.Controllers
{
    public class TicketsController : ApiController
    {
        private readonly INewTicket newTicket;
        private readonly INewReservedTicket newReservedTicket;
        private readonly ITicketRepository ticketRepo;
        private readonly ICancelReservations cancelReservations;

        public TicketsController(
            INewTicket newTicket,
            INewReservedTicket newReservedTicket,
            ITicketRepository ticketRepo,
            ICancelReservations cancelReservations)
        {
            this.newTicket = newTicket;
            this.ticketRepo = ticketRepo;
            this.newReservedTicket = newReservedTicket;
            this.cancelReservations = cancelReservations;
        }

        [HttpPost]
        public async Task<IHttpActionResult> WithReservation(ReservedTicketCreationModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            NewReservedTicketSummary summary = await newReservedTicket
                .NewAsync(new ReservationIdentifier(model.ReservationId));

            if (summary.IsCreated)
            {
                return Ok(summary.TicketOutputReceipt);
            }
            else
            {
                return BadRequest(summary.Message);
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> WithoutReservation(TicketCreationModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await cancelReservations.CancelExpiredReservationsAsync(DateTime.Now);

            NewTicketSummary summary = await newTicket.NewAsync(new Ticket(model.ProjectionId, model.Row, model.Column));

            if (summary.IsCreated)
            {
                return Ok(summary.TicketOutputReceipt);
            }
            else
            {
                return BadRequest(summary.Message);
            }
        }
    }
}
