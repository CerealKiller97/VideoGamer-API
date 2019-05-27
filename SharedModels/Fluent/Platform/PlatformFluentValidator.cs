using FluentValidation;

namespace SharedModels.Fluent.Platform
{
	public class PlatformFluentValidator : AbstractValidator<DTO.Platform>
	{
		public PlatformFluentValidator()
		{
			RuleFor(p => p.PlatformName)
				.NotEmpty()
				.IsInEnum();
		}
	}
}
