using System.ComponentModel.DataAnnotations;

namespace CinemAPI.Models.Input.Projection
{
    public class AvailableSeatsModel
    {
        [Required]
        [Range(0, long.MaxValue, ErrorMessage = "The {0} can not accept negative values")]
        public int ProjectionId { get; set; }
    }
}