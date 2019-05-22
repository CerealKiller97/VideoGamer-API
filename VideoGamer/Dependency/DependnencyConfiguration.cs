using EntityConfiguration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace VideoGamer.Dependency
{
	public class DependencyConfiguration : IDependencyConfiguration
	{
		private readonly IConfiguration _configuration;

		public DependencyConfiguration(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public void Configure(IServiceCollection services)
		{
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
			services.AddCors();
			services.AddDbContext<VideoGamerDbContext>(config =>
			{
				config.UseSqlServer(_configuration.GetConnectionString("VideoGamer"));
			});
		}
	}
}
