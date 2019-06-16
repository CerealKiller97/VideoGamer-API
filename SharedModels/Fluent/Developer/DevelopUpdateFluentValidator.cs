using EntityConfiguration;
using System.Linq;

namespace SharedModels.Fluent.Developer
{
	public class DevelopUpdateFluentValidator : DeveloperFluentValidatior
	{
		private readonly int _id;
		public DevelopUpdateFluentValidator(VideoGamerDbContext context, int id) : base(context)
		{
			_id = id;
		}

		protected override bool BeUniqueName(string Name)
		{
			 return !_context.Developers.Any(d => d.Name == Name && d.Id != _id);
		}

		protected override bool BeUniqueWebSite(string Website)
		{
			return !_context.Developers.Any(d => d.Website == Website && d.Id != _id);
		}
	}
}
