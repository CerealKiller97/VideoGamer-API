using System;

namespace Domain
{
	public class Genre : Model<Guid>
	{
		public string Name { get; set; }
	}
}
