using System.Text;
using System.Threading.Tasks;
using EntityConfiguration;
using EntityConfiguration.Seeders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using VideoGamer.Dependency;

namespace VideoGamer
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
			DependencyConfiguration = new DependencyConfiguration(configuration);
		}

		public IConfiguration Configuration { get; }
		public IDependencyConfiguration DependencyConfiguration { get; }


		public void ConfigureServices(IServiceCollection services)
		{
			// configure strongly typed settings objects
			var appSettingsSection = Configuration.GetSection("AppSettings");
			// services.Configure<AppSettings>(appSettingsSection);

			// configure jwt authentication
			// var appSettings = appSettingsSection.Get<AppSettings>();
			// var key = Encoding.ASCII.GetBytes(appSettings.Secret);
			services.AddAuthentication(x =>
				{
					x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				})
				.AddJwtBearer(x =>
				{
					x.RequireHttpsMetadata = false;
					x.SaveToken = true;
					x.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						// IssuerSigningKey = new SymmetricSecurityKey(key),
						ValidateIssuer = false,
						ValidateAudience = false
					};
				});
			
			DependencyConfiguration.Configure(services);
			services.AddSession(options =>
			{
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true;
			});
		}


		public void Configure(IApplicationBuilder app, IHostingEnvironment env, VideoGamerDbContext context)
		{
            var DbSeeder = new DatabaseSeeder(context);
            DbSeeder.Seed();

            if (env.IsDevelopment())
			{
                app.UseDeveloperExceptionPage();
				app.UseCors(policy =>
				{
					policy.AllowAnyHeader();
					policy.AllowAnyMethod();
					policy.AllowAnyOrigin();
				});
			} else
			{
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}
}


