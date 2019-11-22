using CinemAPI.Data.EF;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Room;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CinemAPI.Data.Implementation
{
    public class RoomRepository : IRoomRepository
    {
        private readonly CinemaDbContext db;

        public RoomRepository(CinemaDbContext db)
        {
            this.db = db;
        }

        public async Task<List<IRoom>> GetAllRoomsAsync()
        {
            return await db.Rooms.ToListAsync<IRoom>();
        }
       

        public async Task<IRoom> GetByCinemaAndNumberAsync(int cinemaId, int number)
        {
            return await db.Rooms.FirstOrDefaultAsync(x => x.CinemaId == cinemaId &&
                                                x.Number == number);
        }

        public async Task<IRoom> GetByIdAsync(int id)
        {
            return await db.Rooms.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertAsync(IRoomCreation room)
        {
            Room newRoom = new Room(room.Number, room.SeatsPerRow, room.Rows, room.CinemaId);

            db.Rooms.Add(newRoom);
            await db.SaveChangesAsync();
        }
    }
}