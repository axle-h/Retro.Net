using Autofac.Builder;
using Retro.Net.Z80.Core;
using Retro.Net.Z80.Core.Interfaces;

namespace Retro.Net.Wiring
{
    /// <summary>
    /// Extensions for Autofac registration.
    /// </summary>
    public static class RegistrationBuilderExtensions
    {
        /// <summary>
        /// Sets up the lifetime of the registration builder to be scoped as a singleton per instance of <see cref="ICpuCore"/>.
        /// </summary>
        /// <typeparam name="TLimit">The type of the limit.</typeparam>
        /// <typeparam name="TActivatorData">The type of the activator data.</typeparam>
        /// <typeparam name="TRegistrationStyle">The type of the registration style.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> InZ80Scope<TLimit, TActivatorData, TRegistrationStyle>(this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> builder)
            => builder.InstancePerOwned<ICpuCore>();
    }
}
