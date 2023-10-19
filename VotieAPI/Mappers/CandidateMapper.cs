using VotieAPI.Data.Entities;
using VotieAPI.Requests;
using VotieAPI.Responses;

namespace VotieAPI.Mappers
{
    public static class CandidateMapper
    {
        public static CreatedCandidateResponse MapToApiResponse(this Candidate candidate)
        {
            return new CreatedCandidateResponse()
            {
                LastName = candidate.LastName,
                Name = candidate.Name,
                Picture = candidate.Picture,
                DistrictName = candidate.District.Name, 
                Id = candidate.Id
            };
        }
        public static Candidate MapToDbEntity(this CreateCandidateRequest input)
        {
            return new Candidate()
            {
                LastName = input.LastName,
                Name = input.Name,
                Picture = input.Picture
            };
        }
        public static Candidate UpdateMapToDbEntity(this UpdateCandidateRequest input) 
        {
            return new Candidate()
            {
                Picture = input.Picture
            };
        }
    }
}
