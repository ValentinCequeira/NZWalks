﻿using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class AddRegionRequestDto
    {
        [Required]//Validation from System...DataAnotations
        [MinLength(3, ErrorMessage ="Code has to be a minumun of 3 characters")]
        [MaxLength(3, ErrorMessage ="Code has to be a maximun of 3 characters")]
        public string Code { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be a maximun of 100 characters")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
