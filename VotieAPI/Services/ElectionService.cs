using Microsoft.EntityFrameworkCore;
using VotieAPI.Data;
using VotieAPI.Data.Entities;
using VotieAPI.Mappers;
using VotieAPI.Requests;
using VotieAPI.Responses;

namespace VotieAPI.Services
{
    public class ElectionService : IElectionService
    {
        private readonly VotieDbContext _votieDbContext;

        public ElectionService(VotieDbContext votieDbContext)
        {
            votieDbContext.Database.EnsureCreated();
            _votieDbContext = votieDbContext;
        }
        public async Task<CreatedElectionResponse> CreateElection(CreateElectionRequest request)
        {
            var electionToCreate = request.MapToDbEntity();
            if (await _votieDbContext.Elections.CountAsync() > 0)
            {
                var electionExists = await _votieDbContext.Elections.FirstOrDefaultAsync(e => e.Name == request.Name && e.StartDate == request.StartDate && e.EndDate == request.EndDate
                && e.District.Id == request.District);
                if (electionExists is not null)
                {
                    throw new Exception("Election cannot be created, because it already registered");
                }
            }
            electionToCreate.District = await _votieDbContext.Districts.FirstAsync(d => d.Id == request.District);
            await _votieDbContext.AddAsync(electionToCreate);
            await _votieDbContext.SaveChangesAsync();
            return electionToCreate.MapToApiResponse();
        }

        public async Task<bool> DeleteElection(int Id)
        {
            var election = await _votieDbContext.Elections.FirstOrDefaultAsync(o => o.Id == Id);
            if (election == null)
                return false;
            _votieDbContext.Remove(election);
            await _votieDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<CreatedElectionResponse> ElectionById(int id)
        {
            var election = await _votieDbContext.Elections.Include(x => x.District).FirstOrDefaultAsync(e => e.Id == id);
            if (election == null)
                return null;
            return election.MapToApiResponse();
        }

        public async Task<IEnumerable<CreatedElectionResponse>> ElectionList()
        {
            var elections = await _votieDbContext.Elections.Include(x => x.District).ToListAsync();
            return elections.Select(e => e.MapToApiResponse());
        }

        public async Task<CreatedElectionResponse> UpdateElection(CreateElectionRequest request, int Id)
        {
            var existingElection = await _votieDbContext.Elections.Include(x => x.District).FirstOrDefaultAsync(c => c.Id == Id);
            if (existingElection is null)
            {
                throw new Exception("Candidate does not exist");
            }
            existingElection.StartDate = request.StartDate;
            existingElection.EndDate = request.EndDate;
            existingElection.Name = request.Name;  
            _votieDbContext.Update(existingElection);
            await _votieDbContext.SaveChangesAsync();
            return existingElection.MapToApiResponse();
        }
    }
}
