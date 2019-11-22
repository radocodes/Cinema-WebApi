using System.ComponentModel.DataAnnotations;

namespace CinemAPI.Models.Input.Movie
{
    public class MovieCreationModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, short.MaxValue, ErrorMessage = "The {0} can not accept negative values")]
        public short DurationMinutes { get; set; }
    }
}