using System;
using System.Collections.Generic;

namespace Domain
{
	public class Genre : AbstractModel<Guid>
	{
		public string Name { get; set; }
		public ICollection<Game<Guid>> Games { get; set; }
	}
}
