using Autofac;
using FluentValidation;

namespace Connexity.BC.Tracking.Domain
{
    using Connexity.BC.Tracking.Domain.Entities;

    /// <summary>
    /// The domain module.
    /// </summary>
    public class DomainModule : Module
    {
        /// <summary>
        /// Override to add registrations to the container.
        /// </summary>
        /// <param name="builder">The builder through which components can be
        /// registered.</param>
        /// <remarks>
        /// Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Tracking>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(this.ThisAssembly).AsClosedTypesOf(typeof(IValidator<>)).AsImplementedInterfaces().InstancePerDependency();
        }
    }
}