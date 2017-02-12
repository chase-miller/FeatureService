using System;
using FeatureService.Models.Domain;

namespace FeatureService.Models.Api
{
    public class FeatureRequest
    {
        public string Id {get; set;}
        public bool Enabled {get; set;}
        public string Lifetime {get; set;}

        public static explicit operator Feature(FeatureRequest feature)
        {
            return new Feature
            {
                Id = feature.Id,
                Enabled = feature.Enabled,
                Lifetime = !string.IsNullOrWhiteSpace(feature.Lifetime) 
                    ? (TimeSpan.Parse(feature.Lifetime) as TimeSpan?)
                    : null
            };
        }
    }
}