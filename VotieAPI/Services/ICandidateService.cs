using VotieAPI.Data.Entities;
using VotieAPI.Requests;
using VotieAPI.Responses;

namespace VotieAPI.Services
{
    public interface ICandidateService
    {
        Task<CreatedCandidateResponse> CreateCandidate(CreateCandidateRequest request);
        Task<IEnumerable<CreatedCandidateResponse>> CandidateList();
        Task<CreatedCandidateResponse> CandidateById(int id);
        Task<CreatedCandidateResponse> UpdateCandidate(UpdateCandidateRequest request, int Id);
        Task<bool> DeleteCandidate(int Id);
    }
}
