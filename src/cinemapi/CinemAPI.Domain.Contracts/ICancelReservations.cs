using CinemAPI.Domain.Contracts.Models;
using System;
using System.Threading.Tasks;

namespace CinemAPI.Domain.Contracts
{
    public interface ICancelReservations
    {
        Task<CancelReservationsSummary> CancelExpiredReservationsAsync(DateTime expirationDate);
    }
}
