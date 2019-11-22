using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CinemAPI.Data.EF;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Ticket;

namespace CinemAPI.Data.Implementation
{
    public class TicketRepository : ITicketRepository
    {
        private readonly CinemaDbContext db;

        public TicketRepository(CinemaDbContext db)
        {
            this.db = db;
        }

        public async Task<ITicket> GetAsync(long projectionId, int row, int column)
        {
            return await db.Tickets.FirstOrDefaultAsync(
                x => x.ProjectionId == projectionId &&
                x.Row == row &&
                x.Column == column);
        }

        public async Task<List<ITicket>> GetAllTicketsAsync()
        {
            return await db.Tickets.ToListAsync<ITicket>();
        }

        public async Task<ITicket> InsertAsync(ITicketCreation ticket)
        {
            Ticket newTicket = new Ticket(ticket.ProjectionId, ticket.Row, ticket.Column);

            Ticket savedTicket = db.Tickets.Add(newTicket);
            await db.SaveChangesAsync();

            return savedTicket;
        }

        public async Task RemoveAsync(int ticketId)
        {
            Ticket ticket = await db.Tickets.FirstOrDefaultAsync(x => x.Id == ticketId);

            if (ticket != null)
            {
                db.Tickets.Remove(ticket);
                await db.SaveChangesAsync();
            }
        }
    }
}
