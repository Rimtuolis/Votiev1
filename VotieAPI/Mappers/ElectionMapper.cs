using VotieAPI.Data.Entities;
using VotieAPI.Requests;
using VotieAPI.Responses;

namespace VotieAPI.Mappers
{
    public static class ElectionMapper
    {
        public static CreatedElectionResponse MapToApiResponse(this Election election)
        {
            return new CreatedElectionResponse()
            {
                Name = election.Name,
                StartDate = election.StartDate.Value,
                EndDate = election.EndDate.Value,
                District = election.District.Name,
                Id = election.Id
            };
        }
        public static Election MapToDbEntity(this CreateElectionRequest input)
        {
            return new Election()
            {
                Name = input.Name,
                StartDate = input.StartDate,
                EndDate = input.EndDate
            };
        }
    }
}
