using System.Runtime.CompilerServices;
using System.Xml.Linq;
using VotieAPI.Data.Entities;
using VotieAPI.Requests;
using VotieAPI.Responses;

namespace VotieAPI.Mappers
{
    public static class VoterMapper
    {
        public static CreatedVoterResponse MapToApiResponse(this Voter voter)
        {
            return new CreatedVoterResponse()
            {
                LastName = voter.LastName,
                Name = voter.Name,
                CreatedAt = voter.Date.Value,
                Id = voter.Id
            };
        }
        public static Voter MapToDbEntity(this CreateVoterRequest input)
        {
            return new Voter()
            {
                LastName = input.LastName,
                Name = input.Name,
                Date = DateTime.Now
            };
        }
    }
}
