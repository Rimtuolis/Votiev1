using VotieAPI.Auth.Entity;

namespace VotieAPI.Data.Entities
{
    public class Voter : VotieUser
    {
        public DateTime? Date { get; set; } 

    }
}
