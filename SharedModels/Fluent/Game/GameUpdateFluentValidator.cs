using EntityConfiguration;
using FluentValidation;
using SharedModels.DTO.Game;
using System;
using System.Linq;

namespace SharedModels.Fluent.Game
{
	public class GameUpdateFluentValidator : AbstractValidator<CreateGameDTO>
	{
		private readonly int _id;
		private readonly VideoGamerDbContext _context;

		public GameUpdateFluentValidator(VideoGamerDbContext context, int id)
		{
			_context = context; 
			_id = id;

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

			RuleFor(g => g.GameMode)
				.NotEmpty()
				.WithMessage("Game mode is required.")
				.IsInEnum()
				.WithMessage("Must be of the offered ones.");

			RuleFor(g => g.PublisherId)
			.NotEmpty()
			.WithMessage("Publisher is required.")
			.Must(ExistInDbPublisher)
			.WithMessage("Publisher doesn't exist.");

			RuleFor(g => g.DeveloperId)
				.NotEmpty()
				.WithMessage("Developer is required.")
				.Must(ExistInDb)
				.WithMessage("Developer doesn't exist.");
		}

		private bool BeUniqueName(string Name) => !_context.Games.Any(g => g.Name == Name && g.Id != _id);

		private bool ExistInDbPublisher(int publisherId) => _context.Publishers.Any(p => p.Id == publisherId);

		private bool ExistInDb(int DeveloperId) => _context.Developers.Any(d => d.Id == DeveloperId);
	}
}
