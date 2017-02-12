using System;

namespace FeatureService.Models.Api
{
    public class FeatureRequest
    {
        public string Id {get; set;}
        public bool Enabled {get; set;}
        public TimeSpan? Lifetime {get; set;}
    }
}