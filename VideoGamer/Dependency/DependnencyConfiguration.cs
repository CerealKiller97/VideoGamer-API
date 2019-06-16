using Aplication.Interfaces;
using EFServices.Services;
using EntityConfiguration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PasswordHashing;
using System.Security.Cryptography;

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
            services.AddTransient<IUserService, EFUserService>()
				    .AddTransient<IDeveloperService, EFDeveloperService>()
			        .AddTransient<IGameService, EFGameService>()
					.AddTransient<IGenreService, EFGenreService>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>((service) => new PasswordHasher(new RNGCryptoServiceProvider()));
        }
    }
}
