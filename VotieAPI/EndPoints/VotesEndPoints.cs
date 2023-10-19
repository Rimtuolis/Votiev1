using O9d.AspNet.FluentValidation;
using VotieAPI.Requests;
using VotieAPI.Responses;
using VotieAPI.Services;

namespace VotieAPI.EndPoints
{
    public class VotesEndPoints
    {
        public static void AddVotesEndPoints(RouteGroupBuilder group)
        {
            group.MapGet("votes", async (IVoteService voteService, CancellationToken cancellationToken) =>
            {
                //return (await dbContext.Voters.ToListAsync(cancellationToken)).Select(o => new VoterDto();
                return await voteService.VoteList();
            });
            group.MapGet("votes/{voteId}", async (int voteId, IVoteService voteService) =>
            {
                var voter = await voteService.VoteById(voteId);
                if (voter == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(voter);
            });
            group.MapPost("votes", async ([Validate] CreateVoteRequest request, IVoteService voteService) =>
            {
                try
                {
                    var createdVote = await voteService.CreateVote(request);

                    return Results.Created<CreatedVoteResponse>($"votes/{createdVote.Id}", createdVote);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }

            });
            group.MapPut("votes/{voteId}", async (int voteId, IVoteService voteService, [Validate] CreateVoteRequest request) =>
            {
                try
                {
                    var createdVote = await voteService.UpdateVote(request, voteId);

                    return Results.Ok(createdVote);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });
            group.MapDelete("votes/{voteId}", async (int voteId, IVoteService voteService) =>
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
