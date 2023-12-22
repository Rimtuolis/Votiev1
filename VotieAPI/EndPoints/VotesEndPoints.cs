using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using O9d.AspNet.FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;
using System.Data;
using VotieAPI.Auth.Entity;
using VotieAPI.Requests;
using VotieAPI.Responses;
using VotieAPI.Services;
using VotieAPI.Data;
using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace VotieAPI.EndPoints
{
    public class VotesEndPoints
    {
        public static void AddVotesEndPoints(RouteGroupBuilder group)
        {
            group.MapGet("votes", [Authorize(Roles = VotieRoles.Admin)] async (IVoteService voteService, CancellationToken cancellationToken) =>
            {
                //return (await dbContext.Voters.ToListAsync(cancellationToken)).Select(o => new VoterDto();
                return await voteService.VoteList();
            });
            group.MapGet("votes/candidates", [Authorize(Roles = VotieRoles.Voter + "," + VotieRoles.Admin)] async (UserManager < VotieUser > userManager,IVoteService voteService, CancellationToken cancellationToken) =>
            {

                return await voteService.CandidateList(userManager);
            });
            group.MapGet("votes/user", [Authorize(Roles = VotieRoles.Voter + "," + VotieRoles.Admin)] async (UserManager<VotieUser> userManager, HttpContext httpContext, IVoteService voteService, CancellationToken cancellationToken) =>
            {
                return await voteService.UserVoteList(httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub));
            });
            group.MapGet("votes/{voteId}", [Authorize(Roles = VotieRoles.Admin)] async (int voteId, IVoteService voteService) =>
            {
                var voter = await voteService.VoteById(voteId);
                if (voter == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(voter);
            });
            group.MapPost("votes",[Authorize(Roles = VotieRoles.Voter)] async ([Validate] CreateVoteRequest request, IVoteService voteService, HttpContext httpContext, UserManager<VotieUser> userManager) =>
            {
                try
                {
                    var createdVote = await voteService.CreateVote(request, httpContext, userManager);

                    return Results.Created<CreatedVoteResponse>($"votes/{createdVote.Id}", createdVote);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }

            });
            group.MapPut("votes/{voteId}", [Authorize(Roles = VotieRoles.Voter)] async (int voteId, IVoteService voteService, [Validate] CreateVoteRequest request,HttpContext httpContext,VotieDbContext dbContext) =>
            {
                try
                {
                    var vote = await dbContext.Votes.FirstOrDefaultAsync(x => x.Id == voteId);
                    if (vote == null)
                    {
                        return Results.NotFound();
                    }
                    if (!httpContext.User.IsInRole(VotieRoles.Admin) && httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub) != vote.VoterId)
                    {
                        return Results.NotFound();
                    }
                    vote.VoterId = httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);
                    vote.CandidateId = request.Candidate;
                    dbContext.Update(vote);
                    await dbContext.SaveChangesAsync();

                    return Results.Ok(vote);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });
            group.MapDelete("votes/{voteId}", [Authorize(Roles = VotieRoles.Admin)] async (int voteId, IVoteService voteService) =>
            {
                var isDeleted = await voteService.DeleteVote(voteId);
                if (!isDeleted)
                {
                    return Results.NotFound();
                }
                return Results.NoContent();
            });
        }
    }
}
