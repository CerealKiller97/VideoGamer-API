using FluentValidation;

namespace SharedModels.Fluent.Developer
{
	public class DeveloperFluentValidatior : AbstractValidator<DTO.Developer>
	{
		public DeveloperFluentValidatior()
		{
			RuleFor(d => d.Name)
				.NotEmpty()
				.MinimumLength(5)
				.MaximumLength(200);
			RuleFor(d => d.HQ)
				.NotEmpty()
				.MinimumLength(5)
				.MaximumLength(200);

			RuleFor(d => d.Founded)
				.NotEmpty();

			RuleFor(d => d.Website)
				.NotEmpty()
				.MinimumLength(10)
				.MaximumLength(255);
		}
	}
}
