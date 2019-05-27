using System.Text.RegularExpressions;
using FluentValidation;
using SharedModels.DTO;

namespace SharedModels.Fluent.User
{
	public class RegisterFluentValidator : AbstractValidator<Register>
	{
		public RegisterFluentValidator()
		{
			RuleFor(u => u.Email)
				.NotEmpty()
				.MaximumLength(150)
				.EmailAddress();

			RuleFor(u => u.FirstName)
				.NotEmpty()
				.MinimumLength(3)
				.MaximumLength(70)
				.Matches(new Regex("^[A-Z][a-z]+$"));

			RuleFor(u => u.LastName)
				.NotEmpty()
				.MinimumLength(3)
				.MaximumLength(70)
				.Matches(new Regex("^[A-Z][a-z]+$"));

			RuleFor(u => u.Password)
				.NotEmpty()
				.MinimumLength(8);
		}
	}
}
