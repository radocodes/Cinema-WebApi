using System;
using System.ComponentModel.DataAnnotations;

namespace CinemAPI.Models.Input.Projection
{
    public class ProjectionCreationModel
    {
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The {0} can not accept negative values")]
        public int RoomId { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The {0} can not accept negative values")]
        public int MovieId { get; set; }

        [Required]
        [DataType(DataType.DateTime, ErrorMessage = "The {0} have to be in valid DateTime format")]
        public DateTime StartDate { get; set; }
    }
}