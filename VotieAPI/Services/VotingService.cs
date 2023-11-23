using Microsoft.EntityFrameworkCore;
using O9d.AspNet.FluentValidation;
using VotieAPI.Data;
using VotieAPI.Mappers;
using VotieAPI.Requests;
using VotieAPI.Responses;

namespace VotieAPI.Services
{
    public class VotingService : IVotingService
    {
        private readonly VotieDbContext _votieDbContext;

        public VotingService(VotieDbContext votieDbContext)
        { 
            votieDbContext.Database.EnsureCreated();
            _votieDbContext = votieDbContext;
        }

        public async Task<CreatedVoterResponse> CreateVoter([Validate] CreateVoterRequest createVoterRequest)
        {
            var voterToCreate = createVoterRequest.MapToDbEntity();
            var userExists = await _votieDbContext.Voters.FirstOrDefaultAsync(voter => voter.Name == createVoterRequest.Name && voter.LastName == createVoterRequest.LastName);
            if (userExists is not null)
            {
                throw new Exception("User cannot be created, because it already registered");
            }
            await _votieDbContext.AddAsync(voterToCreate);
            await _votieDbContext.SaveChangesAsync();
            return voterToCreate.MapToApiResponse();
        }
        //Bool
        public async Task<CreatedVoterResponse> DeleteVoter(string voterId)
        {
            var voter = await _votieDbContext.Voters.FirstOrDefaultAsync(o => o.Id == voterId);
            if (voter == null)
                return null;
            _votieDbContext.Remove(voter);
            await _votieDbContext.SaveChangesAsync();
            return voter.MapToApiResponse();
        }

        public async Task<CreatedVoterResponse> UpdateVoter([Validate] CreateVoterRequest request, string voterId)
        {
            var existingVoter= await _votieDbContext.Voters.FirstOrDefaultAsync(voter => voter.Id == voterId);
            if (existingVoter is null)
            {
                throw new Exception("User does not exist");
            }
            existingVoter.Name = request.Name;
            existingVoter.LastName = request.LastName;
            _votieDbContext.Update(existingVoter);
            await _votieDbContext.SaveChangesAsync();
            return existingVoter.MapToApiResponse();
        }

        public async Task<CreatedVoterResponse?> VoterById(string id)
        {
            var voter = await _votieDbContext.Voters.FirstOrDefaultAsync(o => o.Id == id);
            if (voter == null)
                return null;
            return voter.MapToApiResponse();
        }

        public async Task<IEnumerable<CreatedVoterResponse>> VoterList()
        {
            var voters = await _votieDbContext.Voters.ToListAsync();
            return voters.Select(voter => voter.MapToApiResponse());
        }
    }
}
