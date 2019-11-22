using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Ticket;
using System;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewTicket
{
    public class NewTicketLateValidation : INewTicket
    {
        private readonly IProjectionRepository projectionsRepo;
        private readonly INewTicket newTicket;

        public NewTicketLateValidation(IProjectionRepository projectionsRepo, INewTicket newTicket)
        {
            this.projectionsRepo = projectionsRepo;
            this.newTicket = newTicket;
        }

        public async Task<NewTicketSummary> NewAsync(ITicketCreation ticket)
        {
            var currProjection = await projectionsRepo.GetProjectionByIdAsync(ticket.ProjectionId);

            if (currProjection.StartDate <= DateTime.Now)
            {
                var constraintMessage = "It is too late to buy tickets for this projection! Seats are no longer available.";

                return new NewTicketSummary(false, constraintMessage);
            }
            else
            {
                return await newTicket.NewAsync(ticket);
            }
        }
    }
}
