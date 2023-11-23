using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using VotieAPI.Auth.Entity;

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
