using VotieAPI.Data.Entities;
using VotieAPI.Requests;
using VotieAPI.Responses;

namespace VotieAPI.Services
{
    public interface ICandidateService
    {
        Task<CreatedCandidateResponse> CreateCandidate(CreateCandidateRequest request);
        Task<IEnumerable<CreatedCandidateResponse>> CandidateList();
        Task<CreatedCandidateResponse> CandidateById(string id);
        Task<CreatedCandidateResponse> UpdateCandidate(UpdateCandidateRequest request, string Id);
        Task<bool> DeleteCandidate(string Id);
    }
}
