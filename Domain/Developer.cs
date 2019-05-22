using System;
using System.Collections.Generic;

namespace Domain
{
	public class Developer : AbstractModel<Guid>
	{
		public string Name { get; set; }
		public string HQ { get; set; }
		public DateTime Founded { get; set; }
		public string Website { get; set; }
		public ICollection<Game<Guid>> Games { get; set; }
	}
}
