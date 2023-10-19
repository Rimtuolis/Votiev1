using VotieAPI.Requests;
using VotieAPI.Responses;

namespace VotieAPI.Services
{
    public interface IElectionService
    {
        Task<CreatedElectionResponse> CreateElection(CreateElectionRequest request);
        Task<IEnumerable<CreatedElectionResponse>> ElectionList();
        Task<CreatedElectionResponse> ElectionById(int id);
        Task<CreatedElectionResponse> UpdateElection(CreateElectionRequest request, int Id);
        Task<bool> DeleteElection(int Id);
    }
}
