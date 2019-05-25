using Domain;
using FluentValidation;

namespace SharedModels.Fluent
{
	public class PlatformFluentValidator : AbstractValidator<Platform>
	{
		public PlatformFluentValidator()
		{
			RuleFor(p => p.Name)
				.NotEmpty()
				.IsInEnum();
		}
	}
}
