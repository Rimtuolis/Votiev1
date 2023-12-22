using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using O9d.AspNet.FluentValidation;
using System.Security.Claims;
using VotieAPI.Auth.Entity;
using VotieAPI.Data;
using VotieAPI.Data.Entities;
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

        public async Task<IEnumerable<CandidateResponse>> CandidateList(UserManager<VotieUser> userManager)
        {
            var candidates = await userManager.GetUsersInRoleAsync("Candidate");
            return candidates.Select(candidate => new CandidateResponse { Id = candidate.Id, Name = candidate.Name, LastName = candidate.LastName }).ToList();
        }
        public async Task<CreatedVoteResponse> CreateVote([Validate] CreateVoteRequest request,HttpContext httpContext, UserManager<VotieUser> userManager)
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

            voteToCreate.VoterId = httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            //voteToCreate.Candidate = await _votieDbContext.Candidates.FirstAsync(d => d.Id == request.Candidate);
            //var temp = await userManager.FindByIdAsync(request.Candidate);
            voteToCreate.CandidateId = request.Candidate;
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

        public async Task<CreatedVoteResponse> UpdateVote(CreateVoteRequest request, int Id, HttpContext httpContext)
        {
            var existingVote = await _votieDbContext.Votes
                                                .FirstOrDefaultAsync(c => c.Id == Id);
            if (existingVote is null)
            {
                throw new Exception("Vote does not exist");
            }
            existingVote.VoterId = httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            existingVote.CandidateId = request.Candidate;
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
                                                .FirstOrDefaultAsync(c => c.Id == id);

            if (vote == null)
                return null;
            return vote.MapToApiResponse();
        }

        public async Task<IEnumerable<CreatedVoteResponse>> VoteList()
        {
            var votes = await _votieDbContext.Votes
                                                .ToListAsync();
            return votes.Select(candidate => candidate.MapToApiResponse());
        }

        public async Task<IEnumerable<CreatedVoteResponse>> UserVoteList(string userId)
        {
            var votes = await _votieDbContext.Votes.Where(x=> x.VoterId == userId)
                                                .ToListAsync();
            return votes.Select(candidate => candidate.MapToApiResponse());
        }
    }
}
