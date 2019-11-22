using CinemAPI.Data;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Room;
using CinemAPI.Models.Input.Room;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace CinemAPI.Controllers
{
    public class RoomController : ApiController
    {
        private readonly IRoomRepository roomRepo;

        public RoomController(IRoomRepository roomRepo)
        {
            this.roomRepo = roomRepo;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Create(RoomCreationModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IRoom room = await roomRepo.GetByCinemaAndNumberAsync(model.CinemaId, model.Number);

            if (room == null)
            {
                await roomRepo.InsertAsync(new Room(model.Number, model.SeatsPerRow, model.Rows, model.CinemaId));

                return Ok();
            }

            return BadRequest("Room already exists");
        }
    }
}