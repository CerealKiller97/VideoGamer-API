using Aplication.FileUpload;
using Aplication.Interfaces;
using EFServices.Services;
using EntityConfiguration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVC.Filter;
using System;

namespace MVC
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			services.AddDbContext<VideoGamerDbContext>(config =>
			{
				config.UseSqlServer(Configuration.GetConnectionString("VideoGamer"));
			});

			services.AddTransient<IGameService, EFGameService>()
					.AddTransient<IDeveloperService, EFDeveloperService>();

			services.AddSingleton<IFileService, FileUploadService>();


			services.AddDistributedMemoryCache();

			services.AddSession(options =>
			{
				// Set a short timeout for easy testing.
				options.IdleTimeout = TimeSpan.FromSeconds(10);
				options.Cookie.HttpOnly = true;
				// Make the session cookie essential
				options.Cookie.IsEssential = true;
			});

			services.AddMvc(options =>
			{
				options.Filters.Add(new ModelStateFilter());
			})
			.AddSessionStateTempDataProvider();
			//.AddFluentValidation(options =>
			//{
			//	options.RegisterValidatorsFromAssemblyContaining<Startup>();
			//});

			services.Configure<ApiBehaviorOptions>(opt => opt.SuppressModelStateInvalidFilter = true);


			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			} else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseSession();
			app.UseStaticFiles();
			app.UseCookiePolicy();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
