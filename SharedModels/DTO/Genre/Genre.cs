using System.Collections.Generic;
using SharedModels.DTO.Game;

namespace SharedModels.DTO.Genre
{
    public class Genre : BaseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Game> Games { get; set; }
    }
}
