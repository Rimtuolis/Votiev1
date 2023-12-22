using System.ComponentModel.DataAnnotations;

namespace Votie.Shared
{
    public class Vote
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; } 

        public string Voter { get; set; }
        public string Candidate { get; set; }

    }
    public class VoteRequest
    {
        public string Candidate { get; set; }
        public void Clear() => Candidate = null;
    }
    public class CandidateResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
    }

}
