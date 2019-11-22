using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Projection;
using System.Threading.Tasks;

namespace CinemAPI.Domain.Contracts
{
    public interface INewProjection
    {
        Task<NewProjectionSummary> NewAsync(IProjectionCreation projection);
    }
}