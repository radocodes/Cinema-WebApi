using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using System;
using System.Threading.Tasks;

namespace CinemAPI.Domain.CancelReservation
{
    public class CancelReservations : ICancelReservations
    {
        private readonly IProjectionRepository projectionRepo;
        private readonly IReservationRepository reservationRepo;

        public CancelReservations(IReservationRepository reservationRepo, IProjectionRepository projectionRepo)
        {
            this.reservationRepo = reservationRepo;
            this.projectionRepo = projectionRepo;
        }

        public async Task<CancelReservationsSummary> CancelExpiredReservationsAsync(DateTime expirationDate)
        {

            var expiredReservations = await reservationRepo.GetExpiredReservationsAsync(expirationDate);

            foreach (var reservation in expiredReservations)
            {
                await reservationRepo.RemoveReservationAsync(reservation.Id);

                await projectionRepo.IncreaseAvailableSeatsAsync(reservation.ProjectionId);
            }

            return new CancelReservationsSummary(true);
        }
    }
}
