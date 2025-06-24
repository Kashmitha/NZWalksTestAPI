using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class UpdateRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Minimum characters need 3")]
        [MaxLength(3, ErrorMessage = "Maximum characters need 3")]
        public string Code { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be a maximum 100 characters")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
