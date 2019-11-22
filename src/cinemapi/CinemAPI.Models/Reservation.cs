using System;
using CinemAPI.Models.Contracts.Reservation;

namespace CinemAPI.Models
{
    public class Reservation : IReservation, IReservationCreation
    {
        public Reservation()
        {   
        }

        public Reservation(long projectionId, int row, int column)
        {
            this.ProjectionId = projectionId;
            this.Row = row;
            this.Column = column;
        }

        public Reservation(long projectionId, int row, int column, DateTime expiration)
        {
            this.ProjectionId = projectionId;
            this.Row = row;
            this.Column = column;
            this.Expiration = expiration;
        }

        public long Id { get; set; }

        public long ProjectionId { get; set; }

        public virtual Projection Projection { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }

        public DateTime Expiration { get; set; }
    }
}
