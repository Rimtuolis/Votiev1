using System.ComponentModel.DataAnnotations;
using VotieAPI.Auth.Entity;

namespace VotieAPI.Data.Entities
{
    public class Result
    {
        public int Id { get; set; }
        public float Total { get; set; }
        public float Percent { get; set; }
        public Election Election { get; set; }
        public Candidate Candidate { get; set; }

        [Required]
        public string UserId { get; set; }
        public VotieUser User { get; set; }
    }
}
