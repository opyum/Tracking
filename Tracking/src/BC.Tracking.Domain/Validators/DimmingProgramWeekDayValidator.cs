using FluentValidation;

namespace Connexity.BC.Tracking.Domain.Validators
{
    using static Connexity.BC.Tracking.Domain.Requests.Models.Request;

    public class DimmingProgramWeekDayValidator : AbstractValidator<DimmingProgramWeekDayRequest>
    {
        public DimmingProgramWeekDayValidator()
        {
            this.RuleFor(x => x.DayOfWeek)
                .NotNull().WithMessage("DayOfWeek is required")
                .IsInEnum().WithMessage("DayOfWeek has not a valid value");
            this.RuleFor(x => x.DimmingProgramId).NotEmpty().WithMessage("DimmingProgramId is required");
        }
    }
}