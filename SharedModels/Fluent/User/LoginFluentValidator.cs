using FluentValidation;
using SharedModels.DTO;

namespace SharedModels.Fluent.User
{
	public class LoginFluentValidator : AbstractValidator<Login>
	{
		public LoginFluentValidator()
		{
			RuleFor(u => u.Email)
				.NotEmpty()
				.EmailAddress()
				.MaximumLength(150);

			RuleFor(u => u.Password)
				.NotEmpty()
				.MinimumLength(8);
		}
	}
}
