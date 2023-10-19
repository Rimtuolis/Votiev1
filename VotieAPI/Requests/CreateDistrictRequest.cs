using FluentValidation;

namespace VotieAPI.Requests
{
    public class CreateDistrictRequest
    {
        public string Name { get; set; }
        public string Region { get; set; }
        public class CreateDistrictRequestValidator : AbstractValidator<CreateDistrictRequest>
        {
            public CreateDistrictRequestValidator()
            {
                RuleFor(dto => dto.Name).NotEmpty().NotNull().Length(2,20);
                RuleFor(dto => dto.Region).NotEmpty().NotNull().Length(2,20);
            }
        }
    }
}
