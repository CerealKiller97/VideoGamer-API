using SharedModels.DTO;
using FluentValidation;

namespace SharedModels.Fluent
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
