using System;

namespace SharedModels.DTO
{
    public class CreatePublisherDTO
    {
        public string Name { get; set; }
        public string HQ { get; set; }
        public string ISIN { get; set; }
        public DateTime Founded { get; set; }
        public string Website { get; set; }
    }
}