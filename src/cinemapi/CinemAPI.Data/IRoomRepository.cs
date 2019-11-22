using CinemAPI.Models.Contracts.Room;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemAPI.Data
{
    public interface IRoomRepository
    {
        Task<List<IRoom>> GetAllRoomsAsync();

        Task<IRoom> GetByIdAsync(int id);

        Task<IRoom> GetByCinemaAndNumberAsync(int cinemaId, int number);

        Task InsertAsync(IRoomCreation room);
    }
}