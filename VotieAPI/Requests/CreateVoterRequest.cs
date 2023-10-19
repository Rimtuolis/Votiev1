using FluentValidation;
using VotieAPI.Data.Entities;

namespace VotieAPI.Requests
{
    public class CreateVoterRequest
    {
        public string Name { get; set; }
        public string LastName { get; set; }

       public class CreateVoterRequestValidator : AbstractValidator<CreateVoterRequest>
        {
            public CreateVoterRequestValidator() 
            {
                RuleFor(dto => dto.Name).NotEmpty().NotNull().Length(2,20);
                RuleFor(dto => dto.LastName).NotEmpty().NotNull().Length(2,30);
            }
        }
    }
}
