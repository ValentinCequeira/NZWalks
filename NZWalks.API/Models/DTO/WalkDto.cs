namespace NZWalks.API.Models.DTO
{
    public class WalkDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Descripcion { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        //Add id's to be able to get the types of difficulties and regions insted ID
        public RegionDto Region { get; set; }
        public DifficultyDto Difficulty { get; set; }
    }
}
