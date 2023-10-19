namespace VotieAPI.Data.Entities
{
    public class Candidate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Picture { get; set; }
        public District? District { get; set; }

    }
}
