using EntityConfiguration;
using System.Linq;

namespace SharedModels.Fluent.Genre
{
	public class GenreUpdateFluentValidator : GenreFluentValidator
	{
		private readonly int _id;
		public GenreUpdateFluentValidator(VideoGamerDbContext context, int id) : base(context)
		{
			_id = id;
		}

		protected override bool BeUniqueName(string Name)
		{
			return !_context.Genres.Any(g => g.Name == Name && g.Id != _id);
		}
	}
}
