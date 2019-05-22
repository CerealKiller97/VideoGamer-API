using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Relations
{
    public class GamePlatform<TGameKey, TPlatformKey>
    {
        public TPlatformKey PlatformId { get; set; }
        public Platform Platform { get; set; }

        public TGameKey GameId { get; set; }
        public Game<Guid> Game { get; set; }
	}
}
