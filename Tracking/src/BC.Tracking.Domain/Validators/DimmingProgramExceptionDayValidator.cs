using FluentValidation;

namespace Connexity.BC.Tracking.Domain.Validators
{
    using static Connexity.BC.Tracking.Domain.Requests.Models.Request;

    public class DimmingProgramExceptionDayValidator : AbstractValidator<DimmingProgramExceptionDayRequest>
    {
        public DimmingProgramExceptionDayValidator()
        {
            this.RuleFor(x => x.StartDate)
                .Must((request, item) => item != System.DateOnly.MinValue)
                .WithMessage("StartDate is required");
            this.RuleFor(x => x.EndDate)
                .Must((request, item) => item != System.DateOnly.MinValue)
                .WithMessage("EndDate is required");
            this.RuleFor(x => x)
                .Must((request, item) => request.StartDate == item.EndDate)
                .WithMessage("Only a single day can be an exception");
            this.RuleFor(x => x.DimmingProgramId).NotEmpty().WithMessage("DimmingProgramId is required");
            this.RuleFor(x => x.ExceptionType)
                .NotNull().WithMessage("ExceptionType is required")
                .IsInEnum().WithMessage("ExceptionType has not a valid value");
        }
    }
}