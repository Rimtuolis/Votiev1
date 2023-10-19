using VotieAPI.Data.Entities;
using VotieAPI.Requests;
using VotieAPI.Responses;

namespace VotieAPI.Mappers
{
    public static class DistrictsMapper
    {
        public static CreatedDistrictResponse MapToApiResponse(this District district)
        {
            return new CreatedDistrictResponse()
            {
                Name = district.Name,
                Region = district.Region,
                Id = district.Id
            };
        }
        public static District MapToDbEntity(this CreateDistrictRequest input)
        {
            return new District()
            {
                Name = input.Name,
                Region = input.Region
            };
        }
    }
}
