using O9d.AspNet.FluentValidation;
using VotieAPI.Requests;
using VotieAPI.Responses;
using VotieAPI.Services;

namespace VotieAPI.EndPoints
{
    public class VotersEndPoints
    {
        public static void AddVotersEndPoints(RouteGroupBuilder voterGroup)
        {
            voterGroup.MapGet("voters", async (IVotingService votingService, CancellationToken cancellationToken) =>
            {
                //return (await dbContext.Voters.ToListAsync(cancellationToken)).Select(o => new VoterDto();
                return await votingService.VoterList();
            });
            voterGroup.MapGet("voters/{voterid}", async (int voterId, IVotingService votingService) =>
            {
                var voter = await votingService.VoterById(voterId);
                if (voter == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(voter);
            });
            voterGroup.MapPost("voters", async ([Validate] CreateVoterRequest request, IVotingService votingService) =>
            {
                try
                {
                    var createdVoter = await votingService.CreateVoter(request);

                    return Results.Created<CreatedVoterResponse>($"voters/{createdVoter.Id}", createdVoter);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }

            });
            voterGroup.MapPut("voters/{voterid}", async (int voterId, IVotingService votingService, [Validate] CreateVoterRequest request) =>
            {
                try
                {
                    var createdVoter = await votingService.UpdateVoter(request, voterId);

                    return Results.Ok(createdVoter);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });
            voterGroup.MapPatch("voters/{voterid}", (int voterid) =>
            {

            });
            voterGroup.MapDelete("voters/{voterid}", async (int voterId, IVotingService votingService) =>
            {
                var voter = await votingService.DeleteVoter(voterId);
                if (voter == null)
                {
                    return Results.NotFound();
                }
                return Results.NoContent();
            });
        }
    }
}
