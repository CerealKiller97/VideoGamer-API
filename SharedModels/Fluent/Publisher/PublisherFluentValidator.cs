using FluentValidation;

namespace SharedModels.Fluent.Publisher
{
	public class PublisherFluentValidator : AbstractValidator<DTO.Publisher>
	{
		public PublisherFluentValidator()
		{
			RuleFor(p => p.Name)
				.NotEmpty()
				.MinimumLength(5)
				.MaximumLength(255);

			RuleFor(d => d.HQ)
				.NotEmpty()
				.MinimumLength(5)
				.MaximumLength(200);

			RuleFor(p => p.ISIN)
				.NotEmpty()
				.Length(12);

			RuleFor(p => p.Founded)
				.NotEmpty();

			RuleFor(p => p.Website)
				.NotEmpty()
				.MinimumLength(10)
				.MaximumLength(255);

		}
	}
}
