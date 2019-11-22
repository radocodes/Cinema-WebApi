using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Projection;
using System.Threading.Tasks;

namespace CinemAPI.Domain.Contracts
{
    public interface IAvailableSeatsCount
    {
        Task<AvailableSeatsSummary> GetCountAsync(IProjectionIdentifier projIdentifier);
    }
}
