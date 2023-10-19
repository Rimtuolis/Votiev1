using Microsoft.EntityFrameworkCore;
using O9d.AspNet.FluentValidation;
using VotieAPI.Data;
using VotieAPI.Mappers;
using VotieAPI.Requests;
using VotieAPI.Responses;

namespace VotieAPI.Services
{
    public class VoteService : IVoteService
    {
        private readonly VotieDbContext _votieDbContext;

        public VoteService(VotieDbContext votieDbContext)
        {
            votieDbContext.Database.EnsureCreated();
            _votieDbContext = votieDbContext;
        }
        public async Task<CreatedVoteResponse> CreateVote([Validate] CreateVoteRequest request)
        {
            var voteToCreate = request.MapToDbEntity();
            //if (await _votieDbContext.Votes.CountAsync() > 0)
            //{
            //    var voteExists = await _votieDbContext.Votes.FirstOrDefaultAsync(v => v.Voter.Id == request.Voter && v.Candidate.Id == request.Candidate && req);
            //    if (candidateExists is not null)
            //    {
            //        throw new Exception("Candidate cannot be created, because it already registered");
            //    }
            //}
            voteToCreate.Voter = await _votieDbContext.Voters.FirstAsync(d => d.Id == request.Voter);
            voteToCreate.Candidate = await _votieDbContext.Candidates.FirstAsync(d => d.Id == request.Candidate);
            await _votieDbContext.AddAsync(voteToCreate);
            await _votieDbContext.SaveChangesAsync();
            return voteToCreate.MapToApiResponse();
        }

        public async Task<bool> DeleteVote(int Id)
        {
            var vote = await _votieDbContext.Votes.FirstOrDefaultAsync(o => o.Id == Id);
            if (vote == null)
                return false;
            _votieDbContext.Remove(vote);
            await _votieDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<CreatedVoteResponse> UpdateVote(CreateVoteRequest request, int Id)
        {
            var existingVote = await _votieDbContext.Votes
                                                .Include(v => v.Voter)
                                                .Include(c => c.Candidate)
                                                .ThenInclude(d => d.District)
                                                .FirstOrDefaultAsync(c => c.Id == Id);
            if (existingVote is null)
            {
                throw new Exception("Vote does not exist");
            }
            existingVote.Voter.Id = request.Voter;
            existingVote.Candidate.Id = request.Candidate;
            _votieDbContext.Update(existingVote);
            await _votieDbContext.SaveChangesAsync();
            return existingVote.MapToApiResponse();
        }

        public async Task<CreatedVoteResponse> VoteById(int id)
        {
            //var vote = await _votieDbContext.Votes
            //                                    .Include("Voters.Name")
            //                                    .ThenInclude("Candidates.Name")
            //                                    .FirstOrDefaultAsync(c => c.Id == id);
            var vote = await _votieDbContext.Votes
                                                .Include(c => c.Candidate)
                                                .Include(v => v.Voter)
                                                .FirstOrDefaultAsync(c => c.Id == id);

            if (vote == null)
                return null;
            return vote.MapToApiResponse();
        }

        public async Task<IEnumerable<CreatedVoteResponse>> VoteList()
        {
            var votes = await _votieDbContext.Votes
                                                .Include(c => c.Candidate)
                                                .Include(v => v.Voter)
                                                .ToListAsync();
            return votes.Select(candidate => candidate.MapToApiResponse());
        }
    }
}
