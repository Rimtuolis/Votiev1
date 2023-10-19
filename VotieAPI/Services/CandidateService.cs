using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.EntityFrameworkCore;
using VotieAPI.Data;
using VotieAPI.Data.Entities;
using VotieAPI.Mappers;
using VotieAPI.Requests;
using VotieAPI.Responses;

namespace VotieAPI.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly VotieDbContext _votieDbContext;

        public CandidateService(VotieDbContext votieDbContext)
        {
            votieDbContext.Database.EnsureCreated();
            _votieDbContext = votieDbContext;
        }
        public async Task<CreatedCandidateResponse> CandidateById(int id)
        {
            var candidate = await _votieDbContext.Candidates.Include(x => x.District).FirstOrDefaultAsync(c => c.Id==id);
            if (candidate == null)
                return null;
            return candidate.MapToApiResponse();
        }

        public async Task<IEnumerable<CreatedCandidateResponse>> CandidateList()
        {
            var candidates = await _votieDbContext.Candidates.Include(x => x.District).ToListAsync();
            return candidates.Select(candidate => candidate.MapToApiResponse());
        }


        public async Task<CreatedCandidateResponse> CreateCandidate(CreateCandidateRequest request)
        {
            var candidateToCreate = request.MapToDbEntity();
            if (await _votieDbContext.Candidates.CountAsync() > 0)
            {
                var candidateExists = await _votieDbContext.Candidates.FirstOrDefaultAsync(candidate => candidate.Name == request.Name && candidate.LastName == request.LastName && candidate.District.Id == request.DistrictId);
                if (candidateExists is not null)
                {
                    throw new Exception("Candidate cannot be created, because it already registered");
                }
            }
            candidateToCreate.District = await _votieDbContext.Districts.FirstAsync(d => d.Id == request.DistrictId); 
            await _votieDbContext.AddAsync(candidateToCreate);
            await _votieDbContext.SaveChangesAsync();
            return candidateToCreate.MapToApiResponse();
        }

        public async Task<bool> DeleteCandidate(int Id)
        {
            var candidate = await _votieDbContext.Candidates.FirstOrDefaultAsync(o => o.Id == Id);
            if (candidate == null)
                return false;
            _votieDbContext.Remove(candidate);
            await _votieDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<CreatedCandidateResponse> UpdateCandidate(UpdateCandidateRequest request, int Id)
        {
            var existingCandidate = await _votieDbContext.Candidates.Include(x => x.District).FirstOrDefaultAsync(c => c.Id == Id);
            if (existingCandidate is null)
            {
                throw new Exception("Candidate does not exist");
            }
            existingCandidate.Picture = request.Picture;
            _votieDbContext.Update(existingCandidate);
            await _votieDbContext.SaveChangesAsync();
            return existingCandidate.MapToApiResponse();
        }
    }
}
