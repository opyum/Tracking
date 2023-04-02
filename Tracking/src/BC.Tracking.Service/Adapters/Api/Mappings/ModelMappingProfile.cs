using AutoMapper;

namespace Connexity.BC.Tracking.Service.Adapters.Api.Mappings
{
    using Connexity.BC.Tracking.Domain.Requests;
    using Connexity.BC.Tracking.Service.Adapters.Api.Requests;
    using static Connexity.BC.Tracking.Domain.Requests.Models.Request;
    using static Connexity.BC.Tracking.Service.Adapters.Api.Requests.Models.WebChangeRequest;
    using Entities = Domain.Entities;
    using WebModels = Models;

    public class ModelMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelMappingProfile"/> class.
        /// </summary>
        public ModelMappingProfile()
        {
            this.CreateMap<Entities.Tracking, WebModels.Tracking>();
            this.CreateMap<CreateTrackingWebRequest, InitCreateRequest>();
            this.CreateMap<DimmingProgramWeekDayWebRequest, DimmingProgramWeekDayRequest>().ReverseMap();
            this.CreateMap<DimmingProgramExceptionDayWebRequest, DimmingProgramExceptionDayRequest>().ReverseMap();
        }
    }
}