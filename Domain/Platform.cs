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
	public class Platform : AbstractModel
	{
		public Platforms Name { get; set; }
		public ICollection<GamePlatform> GamePlatforms { get; set; }
    }
}

// id 1 name: PC (Platforms)0