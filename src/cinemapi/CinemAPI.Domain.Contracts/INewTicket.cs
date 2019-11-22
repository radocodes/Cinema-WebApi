﻿using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Ticket;
using System.Threading.Tasks;

namespace CinemAPI.Domain.Contracts
{
    public interface INewTicket
    {
        Task<NewTicketSummary> NewAsync(ITicketCreation ticket);
    }
}
