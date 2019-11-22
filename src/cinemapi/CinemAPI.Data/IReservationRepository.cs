using CinemAPI.Models.Contracts.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Data
{
    public interface IReservationRepository
    {
        Task<IReservation> InsertAsync(IReservation reservation);

        Task<IReservation> GetAsync(long projectionId, int row, int column);

        Task<IReservation> GetByIdAsync(long reservationId);

        Task<List<IReservation>> GetAllAsync();

        Task RemoveReservationAsync(long reservationId);

        Task<List<IReservation>> GetExpiredReservationsAsync(DateTime expirationDate);
    }
}
