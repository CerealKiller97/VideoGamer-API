using System.Text.RegularExpressions;
using FluentValidation;
using SharedModels.DTO;

<<<<<<< HEAD
namespace SharedModels.Fluent.User
=======
namespace SharedModels.Fluent
>>>>>>> b00eedcc014bee4f52c7e012c5e5369fe8f1f4a9
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
