using System.Text;
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
            string key = Configuration.GetSection("JwtKey").Value; 

			byte[] keyBytes = Encoding.ASCII.GetBytes(key);

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
						IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
						ValidateIssuer = false,
						ValidateAudience = false,
					};
				});

			// TODO: HTTP ONLY cookie flag

		//	services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
		//	.AddJwtBearer(options => {
		//	options.Events = new JwtBearerEvents
		//	{
		//		OnMessageReceived = context =>
		//		{
		//			context.Token = context.Request.Cookies["CookieName"];
		//			return Task.CompletedTask;
		//		}
		//	};
		//});

			DependencyConfiguration.Configure(services);
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
			app.UseAuthentication();

			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}
}


