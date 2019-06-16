using System;
using System.IO;
using System.Reflection;
using System.Text;
using EntityConfiguration;
using EntityConfiguration.Seeders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
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

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info
				{
					Version = "v1",
					Title = "VideoGamerAPI",
					Description = "A school project for ASP",
					TermsOfService = "None",
					Contact = new Contact
					{
						Name = "Stefan Bogdanović",
						Email = "bogdanovic.stefan@outlook.com",
						Url = "https://github.com/CerealKiller97"
					},
					License = new License
					{
						Name = "Use under GNU General Public License v2.0",
						Url = "https://github.com/CerealKiller97/VideoGamer-API/blob/master/LICENSE"
					}
				});

				// Set the comments path for the Swagger JSON and UI.
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				c.IncludeXmlComments(xmlPath);
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

			app.UseSwagger();

			// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
			// specifying the Swagger JSON endpoint.
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "VideoGamerAPI V1");
				c.RoutePrefix = string.Empty;
			});
		}
	}
}


