using EntityConfiguration;
using System.Linq;

namespace SharedModels.Fluent.User
{
	public class UserUpdateFluentValidator : RegisterFluentValidator
	{
		private readonly int _id;
		public UserUpdateFluentValidator(VideoGamerDbContext context, int id) : base(context)
		{
			_id = id;
		}

		protected override bool BeUniqueEmailInDatabase(string email)
		{
			return !_context.Users.Any(u => u.Email == email && u.Id != _id);
		}
	}
}
