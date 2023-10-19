using FluentValidation;

namespace VotieAPI.Requests
{
    public class UpdateCandidateRequest
    {
        public string Picture { get; set; }
        public class UpdateCandidateRequestValidator : AbstractValidator<UpdateCandidateRequest>
        {
            public UpdateCandidateRequestValidator()
            {
                RuleFor(dto => dto.Picture).NotEmpty().NotNull().Length(2, 250);
            }
        }
    }

}
