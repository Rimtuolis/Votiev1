using Microsoft.AspNetCore.Identity;
using VotieAPI.Auth.Entity;
using VotieAPI.Requests;
using VotieAPI.Responses;

namespace VotieAPI.Services
{
    public interface IVoteService
    {
        Task<CreatedVoteResponse> CreateVote(CreateVoteRequest request, HttpContext httpContext, UserManager<VotieUser> userManager);
        Task<IEnumerable<CreatedVoteResponse>> VoteList();
        Task<CreatedVoteResponse> VoteById(int id);
        Task<CreatedVoteResponse> UpdateVote(CreateVoteRequest request, int Id, HttpContext httpContext);
        Task<bool> DeleteVote(int Id);
    }
}
