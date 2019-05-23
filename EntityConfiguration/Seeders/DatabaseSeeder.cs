using System.Threading.Tasks;
using Bogus;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace EntityConfiguration.Seeders
{
	public class DatabaseSeeder
	{
		private readonly VideoGamerDbContext _context;

		public DatabaseSeeder(VideoGamerDbContext context) => _context = context;

		public async Task Seed()
		{
			await this.AddUsers();
			await this.AddGames();
		}

		public async Task AddUsers()
		{
			if (! await _context.Users.AnyAsync())
			{
				var testUsers = new Faker<User>()
					.RuleFor(u => u.FirstName, f => f.Name.FirstName())
					.RuleFor(u => u.LastName, f => f.Name.LastName())
					.RuleFor(u => u.Password, f => f.Internet.Password())
					.RuleFor(u => u.Email, f => f.Internet.Email());

				var users = testUsers.Generate(10);

				foreach (var user in users)
				{
					await _context.Users.AddAsync(user);
				}

				await _context.SaveChangesAsync();
			}
		}

		public async Task AddGames()
		{
			if (! await _context.Games.AnyAsync())
			{
				var testGames = new Faker<Game>()
					.RuleFor(g => g.Name, f => f.Name.FirstName());
			}
		}

		public async Task AddGenres()
		{
			if (! await _context.Genres.AnyAsync())
			{
				var testGenres = new Faker<Genre>()
					.RuleFor(g => g.Name, f => f.System.AndroidId());
			}
		}
	}
}
