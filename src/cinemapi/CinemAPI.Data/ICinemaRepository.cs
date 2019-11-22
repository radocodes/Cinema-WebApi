using CinemAPI.Models.Contracts.Cinema;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemAPI.Data
{
    public interface ICinemaRepository
    {
        Task<ICinema> GetByNameAndAddressAsync(string name, string address);

        Task<ICinema> GetByIdAsync(int Id);

        Task InsertAsync(ICinemaCreation cinema);

        Task<List<ICinema>> AllCinemasAsync();
    }
}