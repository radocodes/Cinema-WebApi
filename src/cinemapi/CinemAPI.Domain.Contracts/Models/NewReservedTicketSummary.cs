using CinemAPI.Domain.Contracts.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Domain.Contracts.Models
{
    public class NewReservedTicketSummary
    {
        public NewReservedTicketSummary(bool isCreated)
        {
            this.IsCreated = isCreated;
        }

        public NewReservedTicketSummary(bool status, TicketOutputReceipt ticketOutputReceipt)
            : this(status)
        {
            this.TicketOutputReceipt = ticketOutputReceipt;
        }

        public NewReservedTicketSummary(bool status, string message)
            : this(status)
        {
            this.Message = message;
        }

        public TicketOutputReceipt TicketOutputReceipt { get; set; }

        public bool IsCreated { get; set; }
        public string Message { get; set; }
    }
}
