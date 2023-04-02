using Autofac;
using AutoMapper;

namespace Connexity.BC.Tracking.Service.Adapters
{
    using Connexity.BC.Tracking.Domain.Mappings;
    using Connexity.BC.Tracking.Service.Adapters.Api.Mappings;

    public class MappingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(
                ctx => new MapperConfiguration(
                    cfg =>
                        {
                            cfg.AddProfile<ModelMappingProfile>();
                            cfg.AddProfile<StateMappingProfile>();
                            cfg.AddProfile<DimmingProgramWeekDayMappingProfile>();
                            cfg.AddProfile<DimmingProgramExceptionDayMappingProfile>();
                        }).CreateMapper());
        }
    }
}