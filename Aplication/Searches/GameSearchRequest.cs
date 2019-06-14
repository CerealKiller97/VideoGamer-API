using Domain;

namespace Aplication.Searches
{
    public class GameSearchRequest : BaseSearchRequest
    {
        public string Name { get; set; }
        public string Engine { get; set; }
        public string Publisher { get; set; }
        public PegiAgeRating AgeLabel { get; set; }
        public GameModes GameMode { get; set; }
        public string Developer { get; set; }
    }
}