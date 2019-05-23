using System;
using System.Collections.Generic;

namespace Domain
{
	public class Publisher : AbstractModel
	{
		public string Name { get; set; }
		public string HQ { get; set; }
		public string ISIN { get; set; }
		public DateTime Founded { get; set; }
		public string Website { get; set; }
		public ICollection<Game> Games { get; set; }
	}
}
