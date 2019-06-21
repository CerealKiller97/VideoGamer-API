using Domain;
using System.Collections.Generic;

namespace SharedModels.DTO
{
	public class Platform
	{
		public Platform()
		{
			Platforms = new List<string>();
		}

		public List<string> Platforms { get; set; }
	}
}
