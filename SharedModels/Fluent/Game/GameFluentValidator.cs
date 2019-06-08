using System.Linq;
using EntityConfiguration;
using FluentValidation;

namespace SharedModels.Fluent.Game
{
	public class GameFluentValidator : AbstractValidator<DTO.Game>
	{
		private readonly VideoGamerDbContext _context;
		public GameFluentValidator(VideoGamerDbContext context)
		{
			_context = context;
			
			RuleFor(g => g.Name)
				.NotEmpty()
				.MinimumLength(5)
				.MaximumLength(255);

			RuleFor(g => g.Engine)
				.NotEmpty()
				.MinimumLength(5)
				.MaximumLength(255);

			RuleFor(g => g.DeveloperId)
				.Must(ExistInDb)
				.WithMessage("Developer doesn't exist.");
			
			RuleFor(g => g.ReleaseDate)
				.NotEmpty();
		}

		private bool ExistInDb(int DeveloperId) => _context.Developers.Any(d => d.Id == DeveloperId);
	}
}
