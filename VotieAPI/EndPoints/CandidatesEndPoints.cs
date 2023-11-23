using O9d.AspNet.FluentValidation;
using VotieAPI.Requests;
using VotieAPI.Responses;
using VotieAPI.Services;

namespace VotieAPI.EndPoints
{
    public class CandidatesEndPoints
    {
        public static void AddCandidatesEndPoints(RouteGroupBuilder group)
        {
            group.MapGet("candidates", async (ICandidateService candidateService, CancellationToken cancellationToken) =>
            {
                //return (await dbContext.Voters.ToListAsync(cancellationToken)).Select(o => new VoterDto();
                return await candidateService.CandidateList();
            });
            group.MapGet("candidates/{candidateId}", async (string candidateId, ICandidateService candidateService) =>
            {
                var voter = await candidateService.CandidateById(candidateId);
                if (voter == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(voter);
            });
            group.MapPost("candidates", async ([Validate] CreateCandidateRequest request, ICandidateService candidateService) =>
            {
                try
                {
                    var createdCandidate = await candidateService.CreateCandidate(request);

                    return Results.Created<CreatedCandidateResponse>($"candidates/{createdCandidate.Id}", createdCandidate);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }

            });
            group.MapPatch("candidates/{candidateId}", async (string candidateId, ICandidateService candidateService, [Validate] UpdateCandidateRequest request) =>
            {
                try
                {
                    var createdCandidate = await candidateService.UpdateCandidate(request, candidateId);

                    return Results.Ok(createdCandidate);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });
            group.MapDelete("candidates/{candidateId}", async (string candidateId, ICandidateService candidateService) =>
            {
                var isDeleted = await candidateService.DeleteCandidate(candidateId);
                if (!isDeleted)
                {
                    return Results.NotFound();
                }
                return Results.NoContent();
            });
        }
    }
}
