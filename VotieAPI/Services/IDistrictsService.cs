using VotieAPI.Requests;
using VotieAPI.Responses;

namespace VotieAPI.Services
{
    public interface IDistrictsService
    {
        Task<CreatedDistrictResponse> CreateDistrict(CreateDistrictRequest request);
        Task<List<CreatedDistrictResponse>> DistrictList();
        Task<CreatedDistrictResponse> DistrictById(int id);
        Task<CreatedDistrictResponse> UpdateDistrict(CreateDistrictRequest request, int Id);
        Task<CreatedDistrictResponse> DeleteDistrict(int Id);
        Task<IEnumerable<CreatedVoteResponse>> GetVotesForCandidate(int districtId, int candidateId);
    }
}
