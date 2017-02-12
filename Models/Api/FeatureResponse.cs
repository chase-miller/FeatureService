using System;
using FeatureService.Models.Domain;

namespace FeatureService.Models.Api
{
    public class FeatureResponse
    {
        public string Id {get; set;}
        public bool Enabled {get; set;}
        public DateTime Created {get; set;}
        public TimeSpan? Lifetime {get; set;}
        public DateTime? Expiration { get; set;}

        public bool? IsExpired { get; set;}

        public static explicit operator FeatureResponse(Feature feature)
        {
            return new FeatureResponse
            {
                Id = feature.Id,
                Enabled = feature.Enabled,
                Lifetime = feature.Lifetime,
                Created = feature.Created,
                Expiration = feature.Expiration,
                IsExpired = feature.IsExpired
            };
        }

        public static explicit operator Feature(FeatureResponse feature)
        {
            return new Feature
            {
                Id = feature.Id,
                Enabled = feature.Enabled,
                Lifetime = feature.Lifetime,
                Created = feature.Created
            };
        }
    }
}