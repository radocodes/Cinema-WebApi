using System.ComponentModel.DataAnnotations;

namespace CinemAPI.Models.Input.ReservedTicket
{
    public class ReservedTicketCreationModel
    {
        [Required]
        [Range(0, long.MaxValue, ErrorMessage = "The {0} can not accept negative values")]
        public long ReservationId { get; set; }
    }
}