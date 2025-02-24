namespace NZWalks.API.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Descripcion { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        //Reference to Classes
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }


        //Navigation properties (means Walk will have a Difficulty)
        public Difficulty Difficulty { get; set; }
        public Region Region { get; set; }


    }
}
