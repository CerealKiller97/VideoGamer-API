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
			await AddUsers();
			await AddGames();
			await AddDevelopers();
			await AddGenres();
			await AddPublishers();
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
					.RuleFor(g => g.Name, f => f.Random.Word())
					.RuleFor(g => g.Engine, f => f.Random.Words());
			}
		}
		public async Task AddDevelopers()
		{
			if (! await  _context.Developers.AnyAsync())
			{
				var testDeveloper = new Faker<Developer>()
					.RuleFor(d => d.Name, f => f.Company.CompanyName())
					.RuleFor(d => d.HQ, f => f.Address.City())
					.RuleFor(d => d.Founded, f => f.Date.Recent())
					.RuleFor(d => d.Website, f => f.Internet.DomainName());

				var developers = testDeveloper.Generate(50);
				foreach (var developer in developers)
				{
					await _context.Developers.AddAsync(developer);
				}

				await _context.SaveChangesAsync();
			}
		}

		public async Task AddPublishers()
		{
			if (! await _context.Publishers.AnyAsync())
			{
				var testPublisher = new Faker<Publisher>()
					.RuleFor(d => d.ISIN, f => f.Random.Hash(12))
					.RuleFor(d => d.Name, f => f.Company.CompanyName())
					.RuleFor(d => d.HQ, f => f.Address.City())
					.RuleFor(d => d.Founded, f => f.Date.Recent())
					.RuleFor(d => d.Website, f => f.Internet.DomainName());

				var fakePublishers = testPublisher.Generate(50);

				foreach (var fakePublisher in fakePublishers)
				{
					_context.Publishers.AddAsync(fakePublisher);
				}

				_context.SaveChangesAsync();
			}
		}

		public async Task AddGenres()
		{
			if (! await  _context.Genres.AnyAsync())
			{
				var testGenre = new Faker<Genre>()
					.RuleFor(g => g.Name, f => f.Random.Word());

				var fakeGenres = testGenre.Generate(50);

				foreach (var fakeGenre in fakeGenres)
				{
					await _context.Genres.AddAsync(fakeGenre);
				}

				await _context.SaveChangesAsync();
			}
		}
	}
}
