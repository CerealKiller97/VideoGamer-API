using System.Collections.Generic;

namespace SharedModels.DTO.GamePlatform
{
	public class GamePlatform
	{
		public GamePlatform()
		{
			Platforms = new List<int>();
		}

		public List<int> Platforms { get; set; }
	}
}
