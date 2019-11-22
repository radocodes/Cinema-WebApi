using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Reservation;
using CinemAPI.Models.OutputModels;
using System;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewReservation
{
    public class NewReservationCreation : INewReservation
    {
        private readonly IReservationRepository reservationRepo;
        private readonly IProjectionRepository projectionRepo;
        private readonly IMovieRepository movieRepo;
        private readonly ICinemaRepository cinemaRepo;
        private readonly IRoomRepository roomRepo;

        public NewReservationCreation(
            IReservationRepository reservationRepo,
            IProjectionRepository projectionRepo,
            IMovieRepository movieRepo,
            ICinemaRepository cinemaRepo,
            IRoomRepository roomRepo
            )
        {
            this.reservationRepo = reservationRepo;
            this.projectionRepo = projectionRepo;
            this.movieRepo = movieRepo;
            this.cinemaRepo = cinemaRepo;
            this.roomRepo = roomRepo;
        }

        public async Task<NewReservationSummary> NewAsync(IReservationCreation reservation)
        {
            var currProjection = await projectionRepo.GetProjectionByIdAsync(reservation.ProjectionId);

            var reservationExpiration = currProjection.StartDate - TimeSpan.FromMinutes(10);

            IReservation newReservation = await reservationRepo.InsertAsync(
                new Reservation(
                    reservation.ProjectionId,
                    reservation.Row,
                    reservation.Column,
                    reservationExpiration));

            await projectionRepo.DecreaseAvailableSeatsAsync(reservation.ProjectionId);

            //prepearing for reservation ticket
            var currMovie = await movieRepo.GetByIdAsync(currProjection.MovieId);
            var currRoom = await roomRepo.GetByIdAsync(currProjection.RoomId);
            var currCinema = await cinemaRepo.GetByIdAsync(currRoom.CinemaId);

            ReservationTicket reservationTicket = new ReservationTicket(
                newReservation.Id,
                currProjection.StartDate,
                currMovie.Name,
                currCinema.Name,
                currRoom.Number,
                newReservation.Row,
                newReservation.Column);

            return new NewReservationSummary(true, reservationTicket);
        }
    }
}
