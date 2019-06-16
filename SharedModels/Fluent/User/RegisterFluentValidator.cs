using System.Linq;
using System.Text.RegularExpressions;
using EntityConfiguration;
using FluentValidation;
using SharedModels.DTO;

namespace SharedModels.Fluent.User
{
	public class RegisterFluentValidator : AbstractValidator<DTO.Register>
	{
		private readonly VideoGamerDbContext _context;
		public RegisterFluentValidator(VideoGamerDbContext context)
		{
			_context = context;

            RuleFor(u => u.Email)
                .NotEmpty()
                .WithMessage("Email address is required.")
                .MaximumLength(255)
                .WithMessage("Email address can't be longer than 255 characters.")
                .EmailAddress()
                .WithMessage("Invalid email address.")
				.Must(BeUniqueEmailInDatabase)
				.WithMessage("Email address already exists.");

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

		private bool BeUniqueEmailInDatabase(string email)
		{
			return !_context.Users.Any(u => u.Email ==email);
		}
			
	}
}
