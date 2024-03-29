﻿using CinemAPI.Data;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Cinema;
using CinemAPI.Models.Input.Cinema;
using System.Threading.Tasks;
using System.Web.Http;

namespace CinemAPI.Controllers
{
    public class CinemaController : ApiController
    {
        private readonly ICinemaRepository cinemaRepo;

        public CinemaController(ICinemaRepository cinemaRepo)
        {
            this.cinemaRepo = cinemaRepo;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Create(CinemaCreationModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ICinema cinema = await cinemaRepo.GetByNameAndAddressAsync(model.Name, model.Address);

            if (cinema == null)
            {
                await cinemaRepo.InsertAsync(new Cinema(model.Name, model.Address));

                return Ok();
            }

            return BadRequest("Cinema already exists");
        }
    }
}