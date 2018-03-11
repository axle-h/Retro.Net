using Autofac;
using Autofac.Features.OwnedInstances;
using Retro.Net.Wiring.Interfaces;
using Retro.Net.Z80.Core;
using Retro.Net.Z80.Core.Interfaces;

namespace Retro.Net.Wiring
{
    /// <inheritdoc />
    /// <summary>
    /// Factory for building <see cref="T:Retro.Net.Z80.Core.ICpuCore" /> instances with Autofac.
    /// </summary>
    /// <seealso cref="T:Retro.Net.Wiring.ICpuCoreFactory" />
    public class AutofacCpuCoreFactory : ICpuCoreFactory
    {
        private readonly ILifetimeScope _scope;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutofacCpuCoreFactory" /> class.
        /// </summary>
        public AutofacCpuCoreFactory(ILifetimeScope scope)
        {
            _scope = scope;
        }

        /// <summary>
        /// Gets a new core.
        /// </summary>
        /// <returns></returns>
        public ICpuCore GetNewCore() => _scope.Resolve<Owned<ICpuCore>>().Value; // TODO: not disposing this Owned wrapper will keep the scope around...
    }
}
