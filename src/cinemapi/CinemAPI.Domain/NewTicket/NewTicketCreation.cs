using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Domain.Contracts.OutputModels;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Ticket;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewTicket
{
    public class NewTicketCreation : INewTicket
    {
        private readonly ITicketRepository ticketRepo;
        private readonly IProjectionRepository projectionRepo;
        private readonly IMovieRepository movieRepo;
        private readonly IRoomRepository roomRepo;
        private readonly ICinemaRepository cinemaRepo;

        public NewTicketCreation(
            ITicketRepository ticketRepo,
            IProjectionRepository projectionRepo,
            IMovieRepository movieRepo,
            IRoomRepository roomRepo,
            ICinemaRepository cinemaRepo)
        {
            this.ticketRepo = ticketRepo;
            this.projectionRepo = projectionRepo;
            this.movieRepo = movieRepo;
            this.roomRepo = roomRepo;
            this.cinemaRepo = cinemaRepo;
        }

        public async Task<NewTicketSummary> NewAsync(ITicketCreation reservation)
        {
            ITicket newTicket = await ticketRepo.InsertAsync(
                new Ticket(
                    reservation.ProjectionId,
                    reservation.Row,
                    reservation.Column));

            await projectionRepo.DecreaseAvailableSeatsAsync(reservation.ProjectionId);

            //preparing data for ticketOutputReceipt
            var currProjection = await projectionRepo.GetProjectionByIdAsync(newTicket.ProjectionId);
            var currMovie = await movieRepo.GetByIdAsync(currProjection.MovieId);
            var currRoom = await roomRepo.GetByIdAsync(currProjection.RoomId);
            var currCinema = await cinemaRepo.GetByIdAsync(currRoom.CinemaId);

            TicketOutputReceipt ticketOutputReceipt = new TicketOutputReceipt(
                newTicket.Id,
                currProjection.StartDate,
                currMovie.Name, currCinema.Name,
                currRoom.Number,
                newTicket.Row,
                newTicket.Column);

            return new NewTicketSummary(true, ticketOutputReceipt);
        }
    }
}
