using VotieAPI.Data.Entities;

namespace VotieAPI.Responses
{
    public class CreatedCandidateResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Picture { get; set; }
        public string DistrictName { get; set; }
    }
}
