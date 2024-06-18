using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Services.Jobs.ScopeActivator
{
    public class AppJobActivatorScope([NotNull] IServiceScope serviceScope) : Hangfire.JobActivatorScope
    {
        private readonly IServiceScope _serviceScope = serviceScope ?? throw new ArgumentNullException(nameof(serviceScope));

        public override object Resolve(Type type)
        {
            var res = ActivatorUtilities.GetServiceOrCreateInstance(_serviceScope.ServiceProvider, type);
            return res;
        }

        public override void DisposeScope()
        {
            _serviceScope.Dispose();
        }
    }
}


