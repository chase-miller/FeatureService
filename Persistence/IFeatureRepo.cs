using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeatureService.Models.Domain;

namespace FeatureService.Persistence
{
    public interface IFeatureRepo
    {
        Task<Feature> GetFeature(string featureId);
        Task<IEnumerable<Feature>> GetAllFeatures();
        Task<Feature> CreateFeature(Feature feature);
        Task<Feature> UpdateFeature(string featureId, Feature feature);
        Task<bool> DeleteFeature(string featureId);
    }

    public class InMemoryFeatureRepo : IFeatureRepo
    {
        public static List<Feature> InMemoryDb { get; private set; } = new List<Feature>();
        
        public async Task<Feature> CreateFeature(Feature feature)
        {
            if (InMemoryDb.Any(f => feature.Id == f.Id))
                return null;

            feature.Created = DateTime.UtcNow;
            InMemoryDb.Add(feature);
            return feature;
        }

        public async Task<bool> DeleteFeature(string featureId)
        {
            var features = InMemoryDb.Where(f => f.Id == featureId);

            if (!features.Any())
                return false;

            InMemoryDb.RemoveAll(f => features.Contains(f));

            return true;
        }

        public async Task<IEnumerable<Feature>> GetAllFeatures()
        {
            return InMemoryDb;
        }

        public async Task<Feature> GetFeature(string featureId)
        {
            return InMemoryDb.SingleOrDefault(f => f.Id == featureId);
        }

        public async Task<Feature> UpdateFeature(string featureId, Feature feature)
        {
            var obtainedFeature = InMemoryDb.SingleOrDefault(f => f.Id == featureId);

            if (obtainedFeature == null)
                return null;
            
            feature.Created = obtainedFeature.Created;

            InMemoryDb.RemoveAll(f => f.Id == featureId);
            InMemoryDb.Add(feature);

            return feature;
        }
    }
}