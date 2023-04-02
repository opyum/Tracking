using AutoMapper;

namespace Connexity.BC.Tracking.Domain.Mappings
{
    using static Connexity.BC.Tracking.Domain.Entities.Tracking;
    using static Connexity.BC.Tracking.Domain.Events.Models.TrackingDto;
    using static Connexity.BC.Tracking.Domain.Requests.Models.Request;
    using static Connexity.BC.Tracking.Domain.States.TrackingState;

    public class DimmingProgramWeekDayMappingProfile : Profile
    {
        public DimmingProgramWeekDayMappingProfile()
        {
            this.CreateMap<DimmingProgramWeekDayEntity, DimmingProgramWeekDayState>().ReverseMap();
            this.CreateMap<DimmingProgramWeekDayEntity, DimmingProgramWeekDayDto>().ReverseMap();
            this.CreateMap<DimmingProgramWeekDayEntity, DimmingProgramWeekDayRequest>().ReverseMap();
            this.CreateMap<DimmingProgramWeekDayDto, DimmingProgramWeekDayRequest>().ReverseMap();
        }
    }
}