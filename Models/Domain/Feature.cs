using System;

namespace FeatureService.Models.Domain
{
    public class Feature
    {
        public string Id {get; set;}
        public bool Enabled {get; set;}
        public DateTime Created {get; set;}
        public TimeSpan? Lifetime {get; set;}

        public DateTime? Expiration {
            get
            {
                return Lifetime != null
                    ? Created.Add(Lifetime.Value) as DateTime?
                    : null;
            }
        }
    }
}