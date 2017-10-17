using System;
using Autofac;
using Autofac.Core;
using Autofac.Features.OwnedInstances;
using Retro.Net.Z80.Config;
using Retro.Net.Z80.Core;

namespace Retro.Net.Wiring
{
    /// <inheritdoc />
    /// <summary>
    /// Factory for building <see cref="T:Retro.Net.Z80.Core.ICpuCore" /> instances with Autofac.
    /// </summary>
    /// <seealso cref="T:Retro.Net.Wiring.ICpuCoreFactory" />
    public class AutofacCpuCoreFactory : ICpuCoreFactory
    {
        private readonly ContainerBuilder _builder;
        private readonly Lazy<(IContainer container, ILifetimeScope scope)> _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutofacCpuCoreFactory" /> class.
        /// </summary>
        /// <param name="runtimeConfig">The runtime configuration.</param>
        /// <param name="platformConfig">The platform configuration.</param>
        public AutofacCpuCoreFactory(IRuntimeConfig runtimeConfig, IPlatformConfig platformConfig)
        {
            var z80Module = new Z80Module(runtimeConfig, platformConfig);
            _builder = new ContainerBuilder();
            _builder.RegisterModule(z80Module);
            _container = new Lazy<(IContainer, ILifetimeScope)>(() =>
            {
                var container = _builder.Build();
                var scope = container.BeginLifetimeScope();
                return (container, scope);
            });
        }

        /// <summary>
        /// Registers the Autofac module.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <returns></returns>
        public AutofacCpuCoreFactory RegisterModule(IModule module)
        {
            _builder.RegisterModule(module);
            return this;
        }

        /// <summary>
        /// Gets a new core.
        /// </summary>
        /// <returns></returns>
        public ICpuCore GetNewCore() => _container.Value.scope.Resolve<Owned<ICpuCore>>().Value; // TODO: not disposing this Owned wrapper will keep the scope around...

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            if (_container.IsValueCreated)
            {
                var (container, scope) = _container.Value;
                scope.Dispose();
                container.Dispose();
            }
        }
    }
}
