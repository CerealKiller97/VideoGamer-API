using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace MVC
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateWebHostBuilder(args).Build().Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseKestrel(x =>
				{
					x.Listen(new System.Net.IPAddress(new byte []{ 127, 0, 0, 1 }), 5100);

				})
				.UseStartup<Startup>();
	}
}
