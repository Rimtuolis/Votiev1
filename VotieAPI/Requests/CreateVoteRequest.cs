using FluentValidation;
using VotieAPI.Data.Entities;

namespace VotieAPI.Requests
{
    public class CreateVoteRequest
    {
        public int Voter { get; set; }
        public int Candidate { get; set; }

        public class CreateVoteRequestValidator : AbstractValidator<CreateVoteRequest>
        {
            public CreateVoteRequestValidator()
            {
                RuleFor(dto => dto.Voter).NotEmpty().NotNull();
                RuleFor(dto => dto.Candidate).NotEmpty().NotNull();
            }
        }
    }
}
