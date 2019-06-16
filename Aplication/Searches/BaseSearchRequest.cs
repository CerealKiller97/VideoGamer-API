namespace Aplication.Searches
{
   abstract public class BaseSearchRequest
   {
        public int PerPage { get; set; } = 10;
        public int Page { get; set; } = 1;

		// TODO: public bool Latest { get; set; } = true;
    }
}
