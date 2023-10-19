namespace VotieAPI.Data.Entities
{
    public class Vote
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; } 

        public Voter Voter { get; set; }
        public Candidate Candidate { get; set; }
    }
}
