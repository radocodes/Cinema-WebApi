using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Reservation;
using System;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewReservation
{
    public class NewReservationLateValidation : INewReservation
    {
        private readonly IProjectionRepository projectionsRepo;
        private readonly INewReservation newReservation;

        public NewReservationLateValidation(INewReservation newReservation, IProjectionRepository projectionsRepo)
        {
            this.projectionsRepo = projectionsRepo;
            this.newReservation = newReservation;
        }

        public async Task<NewReservationSummary> NewAsync(IReservationCreation reservation)
        {
            var currProjection = await projectionsRepo.GetProjectionByIdAsync(reservation.ProjectionId);

            var endingTimeToReserve = currProjection.StartDate - TimeSpan.FromMinutes(10);

            if (endingTimeToReserve <= DateTime.Now)
            {
                var constraintMessage = "It is too late to make reservations for this projection! Seats are no longer available.";

                return new NewReservationSummary(false, constraintMessage);
            }
            else
            {
                return await newReservation.NewAsync(reservation);
            }
        }
    }
}
