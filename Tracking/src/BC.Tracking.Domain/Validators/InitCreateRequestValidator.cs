using FluentValidation;
using System.Linq;

namespace Connexity.BC.Tracking.Domain.Validators
{
    using Connexity.BC.Tracking.Domain.Requests;

    public class InitCreateRequestValidator : AbstractValidator<InitCreateRequest>
    {
        public InitCreateRequestValidator()
        {
            this.RuleFor(x => x.Label)
                .NotEmpty().WithMessage("Label is required")
                .MaximumLength(50).WithMessage("Label is too long");

            this.RuleFor(x => x.Code).NotEmpty().WithMessage("Code is required").MaximumLength(50).WithMessage("Code is too long");

            this.RuleFor(x => x.Description).MaximumLength(1000).WithMessage("Description is too long");

            this.RuleFor(x => x.Mode)
                .NotNull().WithMessage("Mode is required")
                .IsInEnum().WithMessage("Mode has not a valid value");

        }
    }
}