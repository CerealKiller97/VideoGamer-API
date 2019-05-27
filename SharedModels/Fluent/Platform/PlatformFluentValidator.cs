using Domain;
using FluentValidation;

namespace SharedModels.Fluent.Platform
{
	public class PlatformFluentValidator : AbstractValidator<Domain.Platform>
	{
		public PlatformFluentValidator()
		{
			RuleFor(p => p.Name)
				.NotEmpty()
				.IsInEnum();
		}
	}
}
