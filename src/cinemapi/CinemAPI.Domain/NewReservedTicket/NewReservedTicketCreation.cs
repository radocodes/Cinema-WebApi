using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Domain.Contracts.OutputModels;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Reservation;
using CinemAPI.Models.Contracts.Ticket;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewReservedTicket
{
    public class NewReservedTicketCreation : INewReservedTicket
    {
        private readonly IReservationRepository reservationRepo;
        private readonly ITicketRepository ticketRepo;
        private readonly IProjectionRepository projectionRepo;
        private readonly IMovieRepository movieRepo;
        private readonly IRoomRepository roomRepo;
        private readonly ICinemaRepository cinemaRepo;

        public NewReservedTicketCreation(
            IReservationRepository reservationRepo,
            ITicketRepository ticketRepo,
            IProjectionRepository projectionRepo,
            IMovieRepository movieRepo,
            IRoomRepository roomRepo,
            ICinemaRepository cinemaRepo)
        {
            this.reservationRepo = reservationRepo;
            this.ticketRepo = ticketRepo;
            this.projectionRepo = projectionRepo;
            this.movieRepo = movieRepo;
            this.roomRepo = roomRepo;
            this.cinemaRepo = cinemaRepo;
        }

        public async Task<NewReservedTicketSummary> NewAsync(IReservationIdentifier reservationIdentifier)
        {
            var reservation = await reservationRepo.GetByIdAsync(reservationIdentifier.ReservationId);

            ITicket newReservedTicket = await ticketRepo.InsertAsync(
                new Ticket(
                    reservation.ProjectionId,
                    reservation.Row,
                    reservation.Column));

            await reservationRepo.RemoveReservationAsync(reservationIdentifier.ReservationId);

            //preparing data for ticketOutputReceipt
            var currProjection = await projectionRepo.GetProjectionByIdAsync(newReservedTicket.ProjectionId);
            var currMovie = await movieRepo.GetByIdAsync(currProjection.MovieId);
            var currRoom = await roomRepo.GetByIdAsync(currProjection.RoomId);
            var currCinema = await cinemaRepo.GetByIdAsync(currRoom.CinemaId);

            TicketOutputReceipt ticketOutputReceipt = new TicketOutputReceipt(
                newReservedTicket.Id,
                currProjection.StartDate,
                currMovie.Name, currCinema.Name,
                currRoom.Number,
                newReservedTicket.Row,
                newReservedTicket.Column);

            return new NewReservedTicketSummary(true, ticketOutputReceipt);
        }
    }
}
