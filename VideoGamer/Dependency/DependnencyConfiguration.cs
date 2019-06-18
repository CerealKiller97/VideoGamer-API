using Aplication.FileUpload;
using Aplication.Interfaces;
using EFServices.Services;
using EntityConfiguration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PasswordHashing;
using System;
using System.Security.Cryptography;
using VideoGamer.Mailer;

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
					.AddTransient<IGenreService, EFGenreService>()
					.AddTransient<IRegisterService, EFRegisterService>()
					.AddTransient<ILoginService, EFLoginService>()
					.AddTransient<IPublisherService, EFPublisherService>()
					.AddTransient<IGameGenreService, EFGameGenreService>();

			services.AddSingleton<IFileService, FileUploadService>();

			var section = _configuration.GetSection("Email");

			var sender = new SmtpEmailService(
				section["host"], 
				Int32.Parse(section["port"]),
				section["fromaddress"],
				section["password"]
			);

			services.AddSingleton<IEmailService>(sender);

			services.AddSingleton
				<IPasswordHasher, PasswordHasher>((service) => new PasswordHasher(new RNGCryptoServiceProvider()));
        }
    }
}
