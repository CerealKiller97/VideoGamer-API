using EntityConfiguration;
using FluentValidation;
using System.Linq;

namespace SharedModels.Fluent.Genre
{
	public class GenreFluentValidator : AbstractValidator<DTO.Genre.CreateGenreDTO>
	{
		protected readonly VideoGamerDbContext _context;
		public GenreFluentValidator(VideoGamerDbContext context)
		{
			_context = context;

			CascadeMode = CascadeMode.StopOnFirstFailure;

			RuleFor(g => g.Name)
				.NotEmpty()
				.WithMessage("Name is required.")
				.MinimumLength(3)
				.WithMessage("Name must be at least 3 characters long.")
				.MaximumLength(255)
				.WithMessage("Name can't be greather than 255 characters long.")
				.Must(BeUniqueName)
				.WithMessage("Name already exitsts.");
		}

		protected virtual bool BeUniqueName(string Name)
		{
			return !_context.Genres.Any(g => g.Name == Name);
		}
	}
}
