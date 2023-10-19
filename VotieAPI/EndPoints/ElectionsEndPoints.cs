using O9d.AspNet.FluentValidation;
using VotieAPI.Requests;
using VotieAPI.Responses;
using VotieAPI.Services;

namespace VotieAPI.EndPoints
{
    public class ElectionsEndPoints
    {
        public static void AddElectionsEndPoints(RouteGroupBuilder group)
        {
            group.MapGet("elections", async (IElectionService electionService, CancellationToken cancellationToken) =>
            {
                //return (await dbContext.Voters.ToListAsync(cancellationToken)).Select(o => new VoterDto();
                return await electionService.ElectionList();
            });

            group.MapGet("elections/{electionId}", async (int electionId, IElectionService electionService) =>
            {
                var voter = await electionService.ElectionById(electionId);
                if (voter == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(voter);
            });
            group.MapPost("elections", async ([Validate] CreateElectionRequest request, IElectionService electionService) =>
            {
                try
                {
                    var createdElection = await electionService.CreateElection(request);

                    return Results.Created<CreatedElectionResponse>($"elections/{createdElection.Id}", createdElection);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }

            });
            group.MapPut("elections/{electionId}", async (int electionId, IElectionService electionService, [Validate] CreateElectionRequest request) =>
            {
                try
                {
                    var createdElection = await electionService.UpdateElection(request, electionId);

                    return Results.Ok(createdElection);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });
            group.MapDelete("elections/{electionId}", async (int electionId, IElectionService electionService) =>
            {
                var isDeleted = await electionService.DeleteElection(electionId);
                if (!isDeleted)
                {
                    return Results.NotFound();
                }
                return Results.NoContent();
            });
        }
    }
}
