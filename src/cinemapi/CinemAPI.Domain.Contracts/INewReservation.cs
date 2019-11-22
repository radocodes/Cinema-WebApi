﻿using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Reservation;
using System.Threading.Tasks;

namespace CinemAPI.Domain.Contracts
{
    public interface INewReservation
    {
        Task<NewReservationSummary> NewAsync(IReservationCreation reservation);
    }
}
