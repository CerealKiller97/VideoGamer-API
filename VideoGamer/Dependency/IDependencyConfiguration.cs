using Microsoft.Extensions.DependencyInjection;

namespace VideoGamer.Dependency
{
	public interface IDependencyConfiguration
	{
		void Configure(IServiceCollection services);
	}
}
