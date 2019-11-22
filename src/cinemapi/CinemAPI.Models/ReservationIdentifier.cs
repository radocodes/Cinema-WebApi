using CinemAPI.Models.Contracts.Reservation;

namespace CinemAPI.Models
{
    public class ReservationIdentifier : IReservationIdentifier
    {
        public ReservationIdentifier()
        {
        }
        public ReservationIdentifier(long reservationId)
        {
            this.ReservationId = reservationId;
        }

        public long ReservationId { get; set; }
    }
}
