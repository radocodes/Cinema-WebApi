using System.ComponentModel.DataAnnotations;

namespace CinemAPI.Models.Input.Ticket
{
    public class TicketCreationModel
    {
        [Required]
        [Range(0, long.MaxValue, ErrorMessage = "The {0} can not accept negative values")]
        public long ProjectionId { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The {0} can not accept negative values")]
        public int Row { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The {0} can not accept negative values")]
        public int Column { get; set; }
    }
}