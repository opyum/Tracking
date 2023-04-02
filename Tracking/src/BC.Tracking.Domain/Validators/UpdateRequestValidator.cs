using FluentValidation;

namespace Connexity.BC.Tracking.Domain.Validators
{
    using Connexity.BC.Tracking.Domain.Requests;

    public class UpdateRequestValidator : AbstractValidator<UpdateRequest>
    {
        public UpdateRequestValidator()
        {
            this.RuleFor(x => x.Id).NotEmpty();
            this.RuleFor(x => x.Label).NotEmpty();
            this.RuleFor(x => x.Code).NotEmpty();
        }
    }
}