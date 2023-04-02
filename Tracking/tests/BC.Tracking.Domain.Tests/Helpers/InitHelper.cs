using Autofac;
using AutoMapper;
using FluentValidation;
using Moq;
using System;
using System.Linq;

namespace Connexity.BC.Tracking.Domain.Tests.Helpers
{
    
    using Connexity.BC.Tracking.Domain.Mappings;
    using Connexity.BC.Tracking.Domain.Ports;
    using Connexity.BC.Tracking.Domain.Requests;
    using Connexity.BC.Tracking.Domain.States;
    using Connexity.BC.Tracking.Domain.Validators;
    using Connexity.BC.Tracking.Service.Adapters.Api.Mappings;
    

    internal static class InitHelper
    {
        ///// <summary>
        ///// Creates the mapper.
        ///// </summary>
        ///// <returns></returns>
        public static IMapper CreateMapper()
        {
            return new MapperConfiguration(
                cfg =>
                {
                    foreach (var t in new[] {
                        typeof(ModelMappingProfile).Assembly,
                        typeof(StateMappingProfile).Assembly
                    }
                        .SelectMany(a => a.GetTypes())
                        .Where(t => !t.IsAbstract && t.IsAssignableTo<Profile>()))
                    {
                        cfg.AddProfile(t);
                    }
                }
            ).CreateMapper();
        }

        public static Type FindType(string type)
        {
            var types = new[] {
                        typeof(ModelMappingProfile).Assembly,
                        typeof(StateMappingProfile).Assembly
                    }
                .SelectMany(a => a.GetTypes())
                .Where(t => !t.IsAbstract);
            return types.SingleOrDefault(t => t.Name.Equals(type, System.StringComparison.InvariantCultureIgnoreCase));
        }

        public static IStorePort CreateStorePort(params TrackingState[] items)
        {
            return new InMemoryStorePort(items);
        }

        public static Mock<INotifyPort> CreateNotifyPort()
        {
            return new Mock<INotifyPort>();
        }

        public static IServiceConfigPort CreateServiceConfigPort()
        {
            return Mock.Of<IServiceConfigPort>();
        }

        public static IValidatorFactory CreateValidatorFactory()
        {
            var mock = new Mock<IValidatorFactory>();
            mock.Setup(x => x.GetValidator<CreateRequest>()).Returns(new CreateRequestValidator());
            mock.Setup(x => x.GetValidator<InitCreateRequest>()).Returns(new InitCreateRequestValidator());
            mock.Setup(x => x.GetValidator<UpdateRequest>()).Returns(new UpdateRequestValidator());

            return mock.Object;
        }
    }
}