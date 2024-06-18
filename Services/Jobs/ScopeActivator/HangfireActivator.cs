using Hangfire;
using Microsoft.Extensions.DependencyInjection;

namespace Services.Jobs.ScopeActivator
{
    public class HangfireActivator(IServiceScopeFactory serviceScopeFactory) : JobActivator
    {
        private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));

        public override JobActivatorScope BeginScope(JobActivatorContext context)
        {
            return new AppJobActivatorScope(_serviceScopeFactory.CreateScope());
        }
    }
}
