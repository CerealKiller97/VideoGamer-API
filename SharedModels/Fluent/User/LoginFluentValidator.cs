using FluentValidation;
using SharedModels.DTO;

namespace SharedModels.Fluent.User
{
	public class LoginFluentValidator : AbstractValidator<Login>
	{
		public LoginFluentValidator()
		{
			CascadeMode = CascadeMode.StopOnFirstFailure;

			RuleFor(u => u.Email)
				.NotEmpty()
				.WithMessage("Email address is required.")
				.EmailAddress()
				.WithMessage("Email address is not valid.")
				.MaximumLength(150)
				.WithMessage("Email address can't be longer than 150 characters.");

			RuleFor(u => u.Password)
				.NotEmpty()
				.WithMessage("Password is required.")
				.MinimumLength(8)
				.WithMessage("Password must be at least 8 characters long.");
		}
	}
}
