using VotieAPI.Data.Entities;
using VotieAPI.Requests;
using VotieAPI.Responses;

namespace VotieAPI.Services
{
    public interface IVotingService
    {
       Task<CreatedVoterResponse> CreateVoter(CreateVoterRequest createVoterRequest);
       Task<IEnumerable<CreatedVoterResponse>> VoterList();
       Task<CreatedVoterResponse?> VoterById(int id);
        Task<CreatedVoterResponse> UpdateVoter(CreateVoterRequest request, int voterId);
        Task<CreatedVoterResponse> DeleteVoter (int voterId);  

    }
}
