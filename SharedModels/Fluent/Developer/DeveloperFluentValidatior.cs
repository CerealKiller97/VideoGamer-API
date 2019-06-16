using System;
using System.Linq;
using EntityConfiguration;
using FluentValidation;
using SharedModels.CustomValidators;

namespace SharedModels.Fluent.Developer
{
	public class DeveloperFluentValidatior : AbstractValidator<DTO.CreateDeveloperDTO>
	{
        protected readonly VideoGamerDbContext _context;
		public DeveloperFluentValidatior(VideoGamerDbContext context)
		{
            _context = context;

            CascadeMode = CascadeMode.StopOnFirstFailure;

			RuleFor(d => d.Name)
				.NotEmpty()
				.WithMessage("Name is required.")
				.MinimumLength(5)
				.WithMessage("Name must be at least 5 characters long.")
				.MaximumLength(200)
				.WithMessage("Name can't be longer than 200 characters long.")
                .Must(BeUniqueName)
                .WithMessage("Name already exists.");
			
			RuleFor(d => d.HQ)
				.NotEmpty()
                .WithMessage("HQ is required.")
                .MinimumLength(5)
				.WithMessage("HQ must be at least 5 characters long.")
				.MaximumLength(200)
				.WithMessage("HQ can't be longer than 200 characters long.");

			RuleFor(d => d.Founded)
				.NotEmpty()
				.GreaterThan(new DateTime(1970,1,1))
				.WithMessage("Date must be greater than 1970.")
				.WithMessage("Foundation date is required.");

			RuleFor(d => d.Website)
				.NotEmpty()
				.WithMessage("Website URL is required.")
				.MinimumLength(10)
				.WithMessage("Website URL must be at least 5 characters long.")
				.MaximumLength(255)
				.WithMessage("Website URL can't be longer than 255 characters long.")
				.SetValidator(new UriValidator("Invalid URL address."))
				.Must(BeUniqueWebSite)
				.WithMessage("Website already exists.");
		}

		protected virtual bool BeValidDate(DateTime dateTime)
		{
			return dateTime != null ?
				!dateTime.Equals(default)
                :false;
		}
		
        protected virtual bool BeUniqueName(string Name)
        {
            return !_context.Developers.Any(d => d.Name == Name);
        }

		protected virtual bool BeUniqueWebSite(string Website)
        {
	        return !_context.Developers.Any(d => d.Website == Website);
        }
    }
}
