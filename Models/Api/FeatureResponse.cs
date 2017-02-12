using System;

namespace FeatureService.Models.Api
{
    public class FeatureResponse
    {
        public string Id {get; set;}
        public bool Enabled {get; set;}
        public DateTime Created {get; set;}
        public TimeSpan? Lifetime {get; set;}
        public DateTime? Expiration { get; set;}
    }
}