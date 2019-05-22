using System;
using System.Collections.Generic;
using Domain.Relations;

namespace Domain
{
	public enum Platforms
	{
		PC,
		PS4,
		PS3,
		PS2,
		PS1,
		Xbox,
		Xbox360,
		XboxOne
	}
	public class Platform : Model<Guid>
	{
		public Platforms Name { get; set; }
        public ICollection<GamePlatform<Guid, Guid>> GamePlatforms { get; set; }
    }
}
