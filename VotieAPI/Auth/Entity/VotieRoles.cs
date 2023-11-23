namespace VotieAPI.Auth.Entity
{
    public static class VotieRoles
    {
        public const string Admin = nameof(Admin);
        public const string Voter = nameof(Voter);
        public const string Candidate = nameof(Candidate);
        public const string Volunteer = nameof(Volunteer);

        public static readonly IReadOnlyCollection<string> All = new[] { Admin, Voter, Candidate, Volunteer };
    }
}
