using Microsoft.AspNetCore.Authorization;
using O9d.AspNet.FluentValidation;
using VotieAPI.Auth.Entity;
using VotieAPI.Requests;
using VotieAPI.Responses;
using VotieAPI.Services;

namespace VotieAPI.EndPoints
{
    public class DistrictsEndPoints
    {
        public static void AddDistrictsEndPoints(RouteGroupBuilder group)
        {
            group.MapGet("districts", [Authorize(Roles = VotieRoles.Admin)] async (IDistrictsService districtsService, CancellationToken cancellationToken) =>
            {
                //return (await dbContext.Voters.ToListAsync(cancellationToken)).Select(o => new VoterDto();
                return await districtsService.DistrictList();
            });
            group.MapGet("districts/{districtId}/candidates/{candidateId}/votes", async (int districtId, string candidateId, IDistrictsService districtsService, CancellationToken cancellationToken) =>
            {
                //return (await dbContext.Voters.ToListAsync(cancellationToken)).Select(o => new VoterDto();
                return await districtsService.GetVotesForCandidate(districtId, candidateId);
            });
            group.MapGet("districts/{districtId}", [Authorize(Roles = VotieRoles.Admin)] async (int districtId, IDistrictsService districtsService) =>
            {
                var district = await districtsService.DistrictById(districtId);
                if (district == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(district);
            });
            group.MapPost("districts", [Authorize(Roles = VotieRoles.Admin)] async ([Validate] CreateDistrictRequest request, IDistrictsService districtsService) =>
            {
                try
                {
                    var createdDistrict = await districtsService.CreateDistrict(request);

                    return Results.Created<CreatedDistrictResponse>($"districts/{createdDistrict.Id}", createdDistrict);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }

            });
            group.MapPut("districts/{districtId}", [Authorize(Roles = VotieRoles.Admin)] async (int districtId, IDistrictsService districtsService, [Validate] CreateDistrictRequest request) =>
            {
                try
                {
                    var createdDistrict = await districtsService.UpdateDistrict(request, districtId);

                    return Results.Ok(createdDistrict);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });
            group.MapPatch("districts/{districtId}", (int districtId) =>
            {

            });
            group.MapDelete("districts/{districtId}", [Authorize(Roles = VotieRoles.Admin)] async (int districtId, IDistrictsService districtsService) =>
            {
                var district = await districtsService.DeleteDistrict(districtId);
                if (district == null)
                {
                    return Results.NotFound();
                }
                return Results.NoContent();
            });
        }
    }
}

