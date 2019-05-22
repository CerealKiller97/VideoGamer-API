using System;
using System.Collections.Generic;

namespace Domain
{
	public class User : AbstractModel<Guid>
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
        public DateTime? ActivatedAt { get; set; }
		public DateTime? DeletedAt { get; set; }

        public DateTime? LastLogin { get; set; }
        public int? UtcOffset { get; set; }

        public ICollection<Game<Guid>> Games { get; set; }
    }
}
