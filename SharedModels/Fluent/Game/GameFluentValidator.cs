using System;
using System.Collections.Generic;
using System.Linq;
using EntityConfiguration;
using FluentValidation;
using SharedModels.DTO.Game;
namespace SharedModels.Fluent.Game
{
	public class GameFluentValidator : AbstractValidator<CreateGameDTO>
	{
		private readonly VideoGamerDbContext _context;
		public GameFluentValidator(VideoGamerDbContext context)
		{
			_context = context;

			CascadeMode = CascadeMode.StopOnFirstFailure;

			RuleFor(g => g.Name)
				.NotEmpty()
				.WithMessage("Name is required.")
				.MinimumLength(5)
				.WithMessage("Name must be at least 5 characters long.")
				.MaximumLength(255)
				.WithMessage("Name can't be longer than 255 characters.")
				.Must(BeUniqueName)
				.WithMessage("Name already exists.");

			RuleFor(g => g.Engine)
				.NotEmpty()
				.WithMessage("Engine is required.")
				.MinimumLength(5)
				.WithMessage("Engine must be at least 5 characters long.")
				.MaximumLength(255)
				.WithMessage("Engine can't be longer than 255 characters.");

			RuleFor(g => g.AgeLabel)
				.NotEmpty()
				.WithMessage("Age label is required.")
				.IsInEnum()
				.WithMessage("Must be of the offered ones.");

			RuleFor(g => g.ReleaseDate)
				.NotEmpty()
				.WithMessage("Release Date is required.")
				.GreaterThan(DateTime.UnixEpoch)
				.WithMessage("Release date must be greater than 1970.");

			RuleFor(g => g.UserId)
				.NotEmpty();

			RuleFor(g => g.GameMode)
				.NotEmpty()
				.WithMessage("Game mode is required.")
				.IsInEnum()
				.WithMessage("Must be of the offered ones.");

			RuleFor(g => g.DeveloperId)
				.NotEmpty()
				.WithMessage("Developer is required.")
				.Must(ExistInDb)
				.WithMessage("Developer doesn't exist.");

			RuleFor(g => g.Genres)
				.NotEmpty()
				.WithMessage("Genres are required.");

			RuleFor(g => g.Platforms)
				.NotEmpty()
				.WithMessage("Platforms are required.");

			RuleFor(g => g.Path)
				.NotEmpty()
				.WithMessage("Image is required.");
		}

		protected virtual bool ExistInDb(int DeveloperId) => _context.Developers.Any(d => d.Id == DeveloperId);

		protected virtual bool ValidGenres(List<int> genres)
		{
			return genres.Count != 0;
		}

		protected virtual bool ValidPlatforms(List<int> platforms)
		{
			return platforms.Count != 0;
		}

		protected virtual bool BeUniqueName(string Name)
		{
			return !_context.Games.Any(g => g.Name == Name);
		}
	}
}
