using System;
using System.Linq;
using Bogus;
using Domain;
using Domain.Relations;

namespace EntityConfiguration.Seeders
{
    public class DatabaseSeeder
	{
		private readonly VideoGamerDbContext _context;

        public DatabaseSeeder(VideoGamerDbContext context) => _context = context; 

		public void Seed()
		{
			AddUsers();
			AddDevelopers();
			AddGenres();
			AddPublishers();
            AddGames();
            AddPlatforms();
            AddGameGenre();
            AddGamePlatform();
		}

        public void AddGameGenre()
        {
            if (!_context.GameGenres.Any())
            {
                var testGameGenre = new Faker<GameGenre>()
                    .RuleFor(g => g.GenreId, f => f.Random.Int(1, 10))
                    .RuleFor(g => g.GameId, f => f.Random.Int(1, 10));

                var data = testGameGenre.Generate(10);

                foreach(var row in data)
                {
                    _context.GameGenres.Add(row);
                }

                _context.SaveChanges();
            }
        }
        public void AddPlatforms()
        {
            if (! _context.Platforms.Any())
            {
                var values = Enum.GetValues(typeof(Platforms)).Cast<Platforms>().ToList();

                foreach (var platform in values)
                {
                    _context.Platforms.Add(new Platform { Name = platform });
                }

                _context.SaveChanges();
            }
        }

		public void AddUsers()
		{
			if (! _context.Users.Any())
			{
				var testUsers = new Faker<User>()
					.RuleFor(u => u.FirstName, f => f.Name.FirstName())
					.RuleFor(u => u.LastName, f => f.Name.LastName())
					.RuleFor(u => u.Password, f => f.Internet.Password())
					.RuleFor(u => u.Email, f => f.Internet.Email());

				var users = testUsers.Generate(10);

				foreach (var user in users)
				{
                    if (_context.Users.Any(u => u.Email == user.Email))
                        continue;

					_context.Users.Add(user);
				}

				 _context.SaveChanges();
			}
		}

		public void AddGames()
		{
		    var s = Enum.GetNames(typeof(PegiAgeRating));

            if (! _context.Games.Any())
			{
                Array values = Enum.GetValues(typeof(PegiAgeRating));
                Random random = new Random();
                PegiAgeRating randomPegiAgeRating = (PegiAgeRating) values.GetValue(random.Next(values.Length));

                Array gameModes = Enum.GetValues(typeof(GameModes));
                Random random3 = new Random();
                GameModes GameModes = (GameModes) gameModes.GetValue(random.Next(gameModes.Length));

                var testGames = new Faker<Game>()
                    .RuleFor(g => g.Name, f => f.Random.Words())
                    .RuleFor(g => g.Engine, f => f.Random.Words())
                    .RuleFor(g => g.AgeLabel, randomPegiAgeRating)
                    .RuleFor(g => g.DeveloperId, f => f.Random.Int(1, 10))
                    .RuleFor(g => g.PublisherId, f => f.Random.Int(1, 10))
                    .RuleFor(g => g.GameMode, GameModes)
                    .RuleFor(g => g.ReleaseDate, f => f.Date.Soon())
                    .RuleFor(g => g.UserId, f => f.Random.Int(1, 10))
                    .RuleFor(g => g.Path, f => f.Image.PicsumUrl());

                var games = testGames.Generate(10);

                foreach (var game in games)
                {
                    if (_context.Games.Any(g => g.Name == game.Name))
                        continue;

                    _context.Games.Add(game);
                }

                _context.SaveChanges();
            }
		}
		public void AddDevelopers()
		{
			if (! _context.Developers.Any())
			{
				var testDeveloper = new Faker<Developer>()
					.RuleFor(d => d.Name, f => f.Random.Words(6))
					.RuleFor(d => d.HQ, f => f.Address.City())
					.RuleFor(d => d.Founded, f => f.Date.Recent())
					.RuleFor(d => d.Website, f => f.Internet.DomainName());

				var developers = testDeveloper.Generate(10);

				foreach (var developer in developers)
				{
                    if (_context.Developers.Any(d => d.Website == developer.Website))
                        continue;

                    if (_context.Developers.Any(d => d.Name == developer.Name))
                        continue;

					_context.Developers.Add(developer);
				}

				 _context.SaveChanges();
			}
		}

		public void AddPublishers()
		{
			if (!  _context.Publishers.Any())
			{
				var testPublisher = new Faker<Publisher>()
					.RuleFor(d => d.ISIN, f => f.Random.Hash(12))
					.RuleFor(d => d.Name, f => f.Company.CompanyName())
					.RuleFor(d => d.HQ, f => f.Address.City())
					.RuleFor(d => d.Founded, f => f.Date.Recent())
					.RuleFor(d => d.Website, f => f.Internet.DomainName());

				var fakePublishers = testPublisher.Generate(10);

				foreach (var fakePublisher in fakePublishers)
				{
                    if (_context.Publishers.Any(p => p.Name == fakePublisher.Name))
                        continue;

                    if (_context.Publishers.Any(p => p.ISIN == fakePublisher.ISIN))
                        continue;

                    if (_context.Publishers.Any(p => p.Website == fakePublisher.Website))
                        continue;

                    _context.Publishers.Add(fakePublisher);
				}

				 _context.SaveChanges();
			}
		}

		public void AddGenres()
		{
			if (! _context.Genres.Any())
			{
				var testGenre = new Faker<Genre>()
					.RuleFor(g => g.Name, f => f.Random.Word());

				var fakeGenres = testGenre.Generate(10);

				foreach (var fakeGenre in fakeGenres)
				{
					 _context.Genres.Add(fakeGenre);
				}

				 _context.SaveChanges();
			}
		}

        public void AddGamePlatform()
        {
            if (! _context.GamePlatforms.Any())
            {
                var gameGenreFaker = new Faker<GamePlatform>()
                    .RuleFor(g => g.GameId, f => f.Random.Int(1, 50))
                    .RuleFor(g => g.PlatformId, f => f.Random.Int(1, 8));

                var data = gameGenreFaker.Generate(10);

                foreach (var row in data)
                {
                    _context.GamePlatforms.Add(row);
                }

                _context.SaveChanges();

            }
        }
	}
}
