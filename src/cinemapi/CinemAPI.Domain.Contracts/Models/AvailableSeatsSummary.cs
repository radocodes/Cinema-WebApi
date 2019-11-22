using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Domain.Contracts.Models
{
    public class AvailableSeatsSummary
    {
        public AvailableSeatsSummary(bool isSuccessfull)
        {
            this.IsSuccessfull = isSuccessfull;
        }

        public AvailableSeatsSummary(bool status, string errorMessage)
            : this(status)
        {
            this.Message = errorMessage;
        }

        public AvailableSeatsSummary(bool status, int availableSeats)
                : this(status)
        {
            this.AvailableSeats = availableSeats;
        }

        public int AvailableSeats { get; set; }

        public bool IsSuccessfull { get; set; }

        public string Message { get; set; }
    }
}
