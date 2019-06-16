using System.Linq;
using System.Text.RegularExpressions;
using EntityConfiguration;
using FluentValidation;

namespace SharedModels.Fluent.User
{
	public class RegisterFluentValidator : AbstractValidator<DTO.Register>
	{
		private readonly VideoGamerDbContext _context;
		public RegisterFluentValidator(VideoGamerDbContext context)
		{
			_context = context;

			CascadeMode = CascadeMode.StopOnFirstFailure;

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
				.WithMessage("Firstname name is required.")
				.MinimumLength(3)
				.WithMessage("Firstname must be at least 3 characters long.")
				.MaximumLength(70)
				.WithMessage("Firstname can't be longer than 70 characters.")
				.Matches(new Regex("^[A-Z][a-z]+$"))
				.WithMessage("First name must start with capital letter.");

			RuleFor(u => u.LastName)
				.NotEmpty()
				.WithMessage("Last name is required.")
				.MinimumLength(3)
				.WithMessage("Lastname must be at least 3 characters long.")
				.MaximumLength(70)
				.WithMessage("Lastname can't be longer than 70 characters.")
				.Matches(new Regex("^[A-Z][a-z]+$"))
				.WithMessage("Last name must start with capital letter.");

			RuleFor(u => u.Password)
				.NotEmpty()
				.WithMessage("Password is required.")
				.MinimumLength(8)
				.WithMessage("Password must be at least 8 characters long.");
		}

		private bool BeUniqueEmailInDatabase(string email)
		{
			return !_context.Users.Any(u => u.Email ==email);
		}
			
	}
}
