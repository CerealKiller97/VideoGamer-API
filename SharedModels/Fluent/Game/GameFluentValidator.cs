using FluentValidation;

namespace SharedModels.Fluent.Game
{
	public class GameFluentValidator : AbstractValidator<DTO.Game>
	{
		public GameFluentValidator()
		{
			RuleFor(g => g.Name)
				.NotEmpty()
				.MinimumLength(5)
				.MaximumLength(255);

			RuleFor(g => g.Engine)
				.NotEmpty()
				.MinimumLength(5)
				.MaximumLength(255);

			RuleFor(g => g.DeveloperId)
				.NotEmpty();
			
			RuleFor(g => g.Developer)
				.NotEmpty();
			
			RuleFor(g => g.ReleaseDate)
				.NotEmpty();
		}
	}
}
