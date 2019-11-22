using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CinemAPI.Data.EF;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Reservation;

namespace CinemAPI.Data.Implementation
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly CinemaDbContext db;

        public ReservationRepository(CinemaDbContext db)
        {
            this.db = db;
        }

        public async Task<IReservation> InsertAsync(IReservation reservation)
        {
            Reservation newReservation = new Reservation(
                reservation.ProjectionId,
                reservation.Row,
                reservation.Column,
                reservation.Expiration);

            Reservation savedReservation = db.Reservations.Add(newReservation);
            await db.SaveChangesAsync();

            return savedReservation;
        }

        public async Task<IReservation> GetAsync(long projectionId, int row, int column)
        {
            return await db.Reservations.FirstOrDefaultAsync(x =>
                x.ProjectionId == projectionId &&
                x.Row == row &&
                x.Column == column);
        }

        public async Task<List<IReservation>> GetAllAsync()
        {
            return await db.Reservations.ToListAsync<IReservation>();
        }

        public async Task RemoveReservationAsync(long reservationId)
        {
            var reservationToRemove = await db.Reservations.FirstOrDefaultAsync(x => x.Id == reservationId);

            if (reservationToRemove != null)
            {
                db.Reservations.Remove(reservationToRemove);
                await db.SaveChangesAsync();
            }
        }

        public async Task<IReservation> GetByIdAsync(long reservationId)
        {
            return await db.Reservations.FirstOrDefaultAsync(x => x.Id == reservationId);
        }

        public async Task<List<IReservation>> GetExpiredReservationsAsync(DateTime expirationDate)
        {
            return await db.Reservations
                .Where(x => x.Expiration <= expirationDate)
                .ToListAsync<IReservation>();
        }
    }
}
