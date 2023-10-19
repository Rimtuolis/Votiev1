using FluentValidation;

namespace VotieAPI.Requests
{
    public class CreateElectionRequest
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int District { get; set;}

        public class CreateElectionRequestValidator : AbstractValidator<CreateElectionRequest>
        {
            public CreateElectionRequestValidator()
            {
                RuleFor(dto => dto.Name).NotEmpty().NotNull().Length(2,50);
                RuleFor(dto => dto.StartDate).NotEmpty().NotNull();
                RuleFor(dto => dto.EndDate).NotEmpty().NotNull();
                RuleFor(dto => dto.District).NotEmpty().NotNull();
            }
        }
    }
}
