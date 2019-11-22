using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Domain.Contracts.Models
{
    public class CancelReservationsSummary
    {
        public CancelReservationsSummary(bool isSuccessfull)
        {
            this.IsSuccessfull = isSuccessfull;
        }

        public bool IsSuccessfull { get; set; }
    }
}
