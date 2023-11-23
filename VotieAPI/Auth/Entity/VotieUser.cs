using Microsoft.AspNetCore.Identity;
using VotieAPI.Data.Entities;

namespace VotieAPI.Auth.Entity
{
    public class VotieUser : IdentityUser
    {
        public bool ForceRelogin { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

    }
}
