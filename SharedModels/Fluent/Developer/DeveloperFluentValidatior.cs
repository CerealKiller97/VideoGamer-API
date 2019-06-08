using FluentValidation;

namespace SharedModels.Fluent.Developer
{
	public class DeveloperFluentValidatior : AbstractValidator<DTO.CreateDeveloperDTO>
	{
		public DeveloperFluentValidatior()
		{

            // CascadeMode = CascadeMode.StopOnFirstFailure;

			RuleFor(d => d.Name)
				.NotEmpty()
                .WithMessage("Name is required.")
				.MinimumLength(5)
				.MaximumLength(200);
			
			RuleFor(d => d.HQ)
				.NotEmpty()
                .WithMessage("HQ is required.")
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
