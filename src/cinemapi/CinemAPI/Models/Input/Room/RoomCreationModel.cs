using System.ComponentModel.DataAnnotations;

namespace CinemAPI.Models.Input.Room
{
    public class RoomCreationModel
    {
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The {0} can not accept negative values")]
        public int Number { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The {0} can not accept negative values")]
        public short SeatsPerRow { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The {0} can not accept negative values")]
        public short Rows { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The {0} can not accept negative values")]
        public int CinemaId { get; set; }
    }
}