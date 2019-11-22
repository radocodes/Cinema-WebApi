using CinemAPI.Models.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Domain.Contracts.Models
{
    public class NewReservationSummary
    {
        public NewReservationSummary(bool isCreated)
        {
            this.IsCreated = isCreated;
        }

        public NewReservationSummary(bool status, ReservationTicket reservationTicket)
            : this(status)
        {
            this.ReservationTicket = reservationTicket;
        }

        public NewReservationSummary(bool status, string message)
            : this(status)
        {
            this.Message = message;
        }

        public ReservationTicket ReservationTicket { get; set; }

        public bool IsCreated { get; set; }
        public string Message { get; set; }
    }
}
