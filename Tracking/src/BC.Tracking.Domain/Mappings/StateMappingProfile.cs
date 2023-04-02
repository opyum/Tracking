using AutoMapper;

namespace Connexity.BC.Tracking.Domain.Mappings
{
    using Connexity.BC.Tracking.Domain.Entities;
    using Connexity.BC.Tracking.Domain.States;
    using static Connexity.BC.Tracking.Domain.Entities.Tracking;
    using static Connexity.BC.Tracking.Domain.States.TrackingState;

    public class StateMappingProfile : Profile
    {
        public StateMappingProfile()
        {
            this.CreateMap<Tracking, TrackingState>().ReverseMap();
            this.CreateMap<DimmingProgramWeekDayEntity, DimmingProgramWeekDayState>().ReverseMap();
            this.CreateMap<DimmingProgramExceptionDayEntity, DimmingProgramExceptionDayState>().ReverseMap();
        }
    }
}