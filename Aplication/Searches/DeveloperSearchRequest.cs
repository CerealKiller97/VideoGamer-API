using System;

namespace Aplication.Searches
{
    public class DeveloperSearchRequest : BaseSearchRequest
    {
        public string Name { get; set; }
        public DateTime? Founded { get; set; }
    }
}