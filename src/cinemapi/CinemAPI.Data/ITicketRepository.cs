using CinemAPI.Models.Contracts.Ticket;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemAPI.Data
{
    public interface ITicketRepository
    {
        Task<List<ITicket>> GetAllTicketsAsync();

        Task<ITicket> GetAsync(long projectionId, int row, int column);

        Task<ITicket> InsertAsync(ITicketCreation ticket);

        Task RemoveAsync(int ticketId);
    }
}
