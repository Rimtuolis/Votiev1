using FluentValidation;
using VotieAPI.Data.Entities;

namespace VotieAPI.Requests
{
    public class CreateCandidateRequest
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Picture { get; set; }
        public int DistrictId { get; set; }

        public class CreateCandidateRequestValidator : AbstractValidator<CreateCandidateRequest>
        {
            public CreateCandidateRequestValidator()
            {
                RuleFor(dto => dto.Name).NotEmpty().NotNull().Length(2, 20);
                RuleFor(dto => dto.LastName).NotEmpty().NotNull().Length(2, 30);
                RuleFor(dto => dto.Picture).NotEmpty().NotNull().Length(2, 250);
                RuleFor(dto => dto.DistrictId).NotEmpty().NotNull();
            }
        }
    }
}
