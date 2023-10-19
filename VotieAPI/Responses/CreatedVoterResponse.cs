namespace VotieAPI.Responses
{
    public class CreatedVoterResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
