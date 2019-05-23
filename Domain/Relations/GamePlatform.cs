using System;

namespace Domain.Relations
{
    public class GamePlatform
    {
        public int PlatformId { get; set; }
        public Platform Platform { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }
	}
}
