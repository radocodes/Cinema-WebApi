using System.ComponentModel.DataAnnotations;

namespace CinemAPI.Models.Input.Cinema
{
    public class CinemaCreationModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }
    }
}