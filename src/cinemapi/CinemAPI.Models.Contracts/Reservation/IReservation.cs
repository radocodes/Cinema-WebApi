using System;

namespace CinemAPI.Models.Contracts.Reservation
{
    public interface IReservation
    {
        long Id { get; set; }

        long ProjectionId { get; set; }

        int Row { get; set; }

        int Column { get; set; }

        DateTime Expiration { get; set; }
    }
}