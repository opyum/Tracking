using AutoMapper;

namespace Connexity.BC.Tracking.Domain.Mappings
{
    using static Connexity.BC.Tracking.Domain.Entities.Tracking;
    using static Connexity.BC.Tracking.Domain.Events.Models.TrackingDto;
    using static Connexity.BC.Tracking.Domain.Requests.Models.Request;
    using static Connexity.BC.Tracking.Domain.States.TrackingState;

    public class DimmingProgramExceptionDayMappingProfile : Profile
    {
        public DimmingProgramExceptionDayMappingProfile()
        {
            this.CreateMap<DimmingProgramExceptionDayEntity, DimmingProgramExceptionDayState>().ReverseMap();
            this.CreateMap<DimmingProgramExceptionDayEntity, DimmingProgramExceptionDayDto>().ReverseMap();
            this.CreateMap<DimmingProgramExceptionDayEntity, DimmingProgramExceptionDayRequest>().ReverseMap();
            this.CreateMap<DimmingProgramExceptionDayDto, DimmingProgramExceptionDayRequest>().ReverseMap();
        }
    }
}