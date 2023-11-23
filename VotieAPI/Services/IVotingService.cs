using VotieAPI.Data.Entities;
using VotieAPI.Requests;
using VotieAPI.Responses;

namespace VotieAPI.Services
{
    public interface IVotingService
    {
       Task<CreatedVoterResponse> CreateVoter(CreateVoterRequest createVoterRequest);
       Task<IEnumerable<CreatedVoterResponse>> VoterList();
       Task<CreatedVoterResponse?> VoterById(string id);
        Task<CreatedVoterResponse> UpdateVoter(CreateVoterRequest request, string voterId);
        Task<CreatedVoterResponse> DeleteVoter (string voterId);  

    }
}
