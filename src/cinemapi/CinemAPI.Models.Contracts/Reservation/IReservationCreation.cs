﻿namespace CinemAPI.Models.Contracts.Reservation
{
    public interface IReservationCreation
    {
        long ProjectionId { get; set; }

        int Row { get; set; }

        int Column { get; set; }
    }
}
