using System;

namespace Domain
{
	public enum GameModes
	{
		SinglePlayer = 1,
		MultiPlayer  = 1 << 2,
		Cooperative  = 1 << 3,
		SingleMultiPlayer = SinglePlayer | MultiPlayer
	}
	//   8421 => 0000 => 0001 => 0010
	public class GameMode : AbstractModel<Guid>
	{

	}
}
