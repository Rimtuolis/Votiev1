using VotieAPI.Requests;
using VotieAPI.Responses;

namespace VotieAPI.Services
{
    public interface IVoteService
    {
        Task<CreatedVoteResponse> CreateVote(CreateVoteRequest request);
        Task<IEnumerable<CreatedVoteResponse>> VoteList();
        Task<CreatedVoteResponse> VoteById(int id);
        Task<CreatedVoteResponse> UpdateVote(CreateVoteRequest request, int Id);
        Task<bool> DeleteVote(int Id);
    }
}
