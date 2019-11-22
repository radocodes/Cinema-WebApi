using CinemAPI.Models.Contracts.Projection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Models
{
    public class ProjectionIdentifier : IProjectionIdentifier
    {
        public ProjectionIdentifier(long projectionId)
        {
            this.ProjectionId = projectionId;
        }

        public long ProjectionId { get; set ; }
    }
}
