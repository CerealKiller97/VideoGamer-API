using EntityConfiguration;
using FluentValidation;
using System;
using System.Linq;

namespace SharedModels.Fluent.Publisher
{
	public class PublisherFluentValidator : AbstractValidator<DTO.CreatePublisherDTO>
	{
		protected readonly VideoGamerDbContext _context;
		public PublisherFluentValidator(VideoGamerDbContext context)
		{
			_context = context;

			CascadeMode = CascadeMode.StopOnFirstFailure;

			RuleFor(p => p.Name)
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
				.WithMessage("Name can't be longer than 200 characters long.");

			RuleFor(p => p.ISIN)
				.NotEmpty()
				.WithMessage("ISIN is required.")
				.Length(12)
				.WithMessage("ISIN must be exactly 12 characters.")
				.Must(BeUniqueISIN)
				.WithMessage("ISIN already exists.");

			RuleFor(p => p.Founded)
				.NotEmpty();

			RuleFor(p => p.Website)
				.NotEmpty()
				.WithMessage("Website is required.")
				.MinimumLength(10)
				.WithMessage("Website must be at least 10 characters long.")
				.MaximumLength(255)
				.WithMessage("Website can't be longer than 255 characters long.")
				.Must(BeUniqueWebSite)
				.WithMessage("Website already exists.");

		}
		protected virtual bool BeValidDate(DateTime dateTime)
		{
			return dateTime != null ?
				!dateTime.Equals(default)
				: false;
		}

		protected virtual bool BeUniqueName(string Name)
		{
			return !_context.Publishers.Any(d => d.Name == Name);
		}

		protected virtual bool BeUniqueWebSite(string Website)
		{
			return !_context.Publishers.Any(d => d.Website == Website);
		}

		protected virtual bool BeUniqueISIN(string Isin)
		{
			return !_context.Publishers.Any(d => d.ISIN == Isin);
		}
	}
}
