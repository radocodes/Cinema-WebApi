using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Domain.Contracts.OutputModels
{
    public class TicketOutputReceipt
    {
        public TicketOutputReceipt(
            long ticketId,
            DateTime projectionStartDate,
            string movieName,
            string cinemaName,
            int roomNumber,
            int row,
            int column)
        {
            this.TicketId = ticketId;
            this.ProjectionStartDate = projectionStartDate;
            this.MovieName = movieName;
            this.CinemaName = cinemaName;
            this.RoomNumber = roomNumber;
            this.Row = row;
            this.Column = column;
        }

        public long TicketId { get; set; }

        public DateTime ProjectionStartDate { get; set; }

        public string MovieName { get; set; }

        public string CinemaName { get; set; }

        public int RoomNumber { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }
    }
}
