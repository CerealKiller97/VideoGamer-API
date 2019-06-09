namespace Aplication.Searches
{
    public class PublisherSearchRequest : BaseSearchRequest
    {
        public string Name { get; set; }
        public string ISIN { get; set; }
    }
}