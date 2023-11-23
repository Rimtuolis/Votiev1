using FluentValidation;
using VotieAPI.Data.Entities;

namespace VotieAPI.Requests
{
    public class CreateVoteRequest
    {
        public string Candidate { get; set; }

        public class CreateVoteRequestValidator : AbstractValidator<CreateVoteRequest>
        {
            public CreateVoteRequestValidator()
            {
                RuleFor(dto => dto.Candidate).NotEmpty().NotNull();
            }
        }
    }
}
