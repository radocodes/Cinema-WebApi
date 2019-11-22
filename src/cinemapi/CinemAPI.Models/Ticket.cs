using CinemAPI.Models.Contracts.Ticket;

namespace CinemAPI.Models
{
    public class Ticket : ITicket, ITicketCreation
    {
        public Ticket()
        {
        }

        public Ticket(long ProjectionId, int Row, int Column)
        {
            this.ProjectionId = ProjectionId;
            this.Row = Row;
            this.Column = Column;
        }

        public long Id { get; set; } 

        public long ProjectionId { get; set; }

        public virtual Projection Projection { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }
    }
}
