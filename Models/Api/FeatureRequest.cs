using System;
using FeatureService.Models.Domain;

namespace FeatureService.Models.Api
{
    public class FeatureRequest
    {
        public string Id {get; set;}
        public bool Enabled {get; set;}
        public TimeSpan? Lifetime {get; set;}

        public static explicit operator FeatureRequest(Feature feature)
        {
            return new FeatureRequest
            {
                Id = feature.Id,
                Enabled = feature.Enabled,
                Lifetime = feature.Lifetime
            };
        }

        public static explicit operator Feature(FeatureRequest feature)
        {
            return new Feature
            {
                Id = feature.Id,
                Enabled = feature.Enabled,
                Lifetime = feature.Lifetime
            };
        }
    }
}