using VotieAPI.Data.Entities;

namespace VotieAPI.Responses
{
    public class CreatedVoteResponse
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public string Voter { get; set; }
        public string Candidate { get; set; }
    }
    public class CandidateResponse
    {
        public string Id { get; set;}
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
