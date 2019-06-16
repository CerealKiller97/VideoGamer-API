using EntityConfiguration;
using System.Linq;

namespace SharedModels.Fluent.Publisher
{
	public class PublisherUpdateFluentValidator : PublisherFluentValidator
	{
		private readonly int _id;
		public PublisherUpdateFluentValidator(VideoGamerDbContext context, int id) : base(context)
		{
			_id = id;
		}

		protected override bool BeUniqueName(string Name)
		{
			return !_context.Publishers.Any(p => p.Name == Name && p.Id != _id);
		}

		protected override bool BeUniqueWebSite(string Website)
		{
			return !_context.Publishers.Any(p => p.Website == Website && p.Id != _id);
		}

		protected override bool BeUniqueISIN(string Isin)
		{
			return !_context.Publishers.Any(p => p.ISIN == Isin && p.Id != _id);
		}
	}
}
