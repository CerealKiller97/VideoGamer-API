using FluentValidation;
using SharedModels.DTO;

<<<<<<< HEAD
namespace SharedModels.Fluent.User
=======
namespace SharedModels.Fluent
>>>>>>> b00eedcc014bee4f52c7e012c5e5369fe8f1f4a9
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
