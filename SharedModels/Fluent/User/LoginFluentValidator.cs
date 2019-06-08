using FluentValidation;
using SharedModels.DTO;

namespace SharedModels.Fluent.User
{
	public class LoginFluentValidator : AbstractValidator<DTO.Login>
	{
		public LoginFluentValidator()
		{
			RuleFor(u => u.Email)
				.NotEmpty()
				.WithMessage("Email address is required.")
				.EmailAddress()
				.MaximumLength(150);

			RuleFor(u => u.Password)
				.NotEmpty()
				.WithMessage("Password is required.")
				.MinimumLength(8)
				.WithMessage("Password must be at least 8 characters long.");

		}
	}
}
