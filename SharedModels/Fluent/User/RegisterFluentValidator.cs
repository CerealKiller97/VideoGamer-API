using System.Text.RegularExpressions;
using FluentValidation;
using SharedModels.DTO;

namespace SharedModels.Fluent.User
{
	public class RegisterFluentValidator : AbstractValidator<DTO.Register>
	{
		public RegisterFluentValidator()
		{
			RuleFor(u => u.Email)
				.NotEmpty()
				.WithMessage("Email address is required.")
				.MaximumLength(150)
				.EmailAddress();

			RuleFor(u => u.FirstName)
				.NotEmpty()
				.WithMessage("First name is required.")
				.MinimumLength(3)
				.MaximumLength(70)
				.Matches(new Regex("^[A-Z][a-z]+$"))
				.WithMessage("First name must start with capital letter.");

			RuleFor(u => u.LastName)
				.NotEmpty()
				.WithMessage("Last name is required.")
				.MinimumLength(3)
				.MaximumLength(70)
				.Matches(new Regex("^[A-Z][a-z]+$"))
				.WithMessage("Last name must start with capital letter.");

			RuleFor(u => u.Password)
				.NotEmpty()
				.WithMessage("Password is required.")
				.MinimumLength(8);
		}
	}
}
