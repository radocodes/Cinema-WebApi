using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Reservation;
using CinemAPI.Models.Input.Reservation;
using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CinemAPI.Controllers
{
    public class ReservationController : ApiController
    {
        private readonly INewReservation newReservation;
        private readonly IReservationRepository reservationRepo;
        private readonly IProjectionRepository projectionRepo;


        public ReservationController(INewReservation newReservation, IReservationRepository reservationRepo, IProjectionRepository projectionRepo)
        {
            this.newReservation = newReservation;
            this.reservationRepo = reservationRepo;
            this.projectionRepo = projectionRepo;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Create(ReservationCreationModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            NewReservationSummary summary = await newReservation
                .NewAsync(new Reservation(model.ProjectionId, model.Row, model.Column));

            if (summary.IsCreated)
            {
                return Ok(summary.ReservationTicket);
            }
            else
            {
                return BadRequest(summary.Message);
            }
        }
    }
}
