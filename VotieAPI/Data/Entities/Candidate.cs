using VotieAPI.Auth.Entity;

namespace VotieAPI.Data.Entities
{
    public class Candidate : VotieUser
    {
        public string Picture { get; set; }
        public District? District { get; set; }

    }
}
