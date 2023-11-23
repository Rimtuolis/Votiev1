using Microsoft.EntityFrameworkCore;
using VotieAPI.Data;
using VotieAPI.Mappers;
using VotieAPI.Requests;
using VotieAPI.Responses;

namespace VotieAPI.Services
{
    public class DistrictsService : IDistrictsService
    {
        private readonly VotieDbContext _votieDbContext;

        public DistrictsService(VotieDbContext votieDbContext)
        {
            votieDbContext.Database.EnsureCreated();
            _votieDbContext = votieDbContext;
        }
        public async Task<CreatedDistrictResponse> CreateDistrict(CreateDistrictRequest request)
        {
            var districtToCreate = request.MapToDbEntity();
            var districtExists = await _votieDbContext.Districts.FirstOrDefaultAsync(o => o.Name == request.Name && o.Region == request.Region);
            if (districtExists is not null)
            {
                throw new Exception("District cannot be created, because it already registered");
            }
            await _votieDbContext.AddAsync(districtToCreate);
            await _votieDbContext.SaveChangesAsync();
            return districtToCreate.MapToApiResponse();
        }

        public async Task<CreatedDistrictResponse> DeleteDistrict(int Id)
        {
            var district = await _votieDbContext.Districts.FirstOrDefaultAsync(o => o.Id == Id);
            if (district == null)
                return null;
            _votieDbContext.Remove(district);
            await _votieDbContext.SaveChangesAsync();
            return district.MapToApiResponse();
        }

        public async Task<CreatedDistrictResponse> DistrictById(int id)
        {
            var district = await _votieDbContext.Districts.FirstOrDefaultAsync(o => o.Id == id);
            if (district == null)
                return null;
            return district.MapToApiResponse();
        }

        public async Task<List<CreatedDistrictResponse>> DistrictList()
        {
            var districts = await _votieDbContext.Districts.ToListAsync();
            List<CreatedDistrictResponse> createdDistrictResponse = new List<CreatedDistrictResponse>();
            foreach (var district in districts)
            {
                createdDistrictResponse.Add(district.MapToApiResponse());
            }
            return createdDistrictResponse;
        }

        public async Task<IEnumerable<CreatedVoteResponse>> GetVotesForCandidate(int districtId, string candidateId)
        {
            var votes = await _votieDbContext.Votes.Where(c => c.Candidate.Id == candidateId && c.Candidate.District.Id == districtId)
                .Include(c => c.Candidate)
                .Include(v => v.Voter).ToListAsync();
            return votes.Select(vote => vote.MapToApiResponse());
        }

        public async Task<CreatedDistrictResponse> UpdateDistrict(CreateDistrictRequest request, int Id)
        {
            var districtToUpdate = request.MapToDbEntity();
            var districtExists = await _votieDbContext.Districts.FirstOrDefaultAsync(voter => voter.Id == Id);
            if (districtExists is null)
            {
                throw new Exception("District does not exist");
            }
            districtExists.Name = districtToUpdate.Name;
            districtExists.Region = districtToUpdate.Region;
            _votieDbContext.Update(districtExists);
            await _votieDbContext.SaveChangesAsync();
            return districtExists.MapToApiResponse();
        }
    }
}
