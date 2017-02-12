using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FeatureService.Models.Api;

namespace FeatureService.Services
{
    public interface IFeatureService
    {
        Task<Feature> GetFeature(string featureId);
        Task<IEnumerable<Feature>> GetAllFeatures();
        Task<Feature> CreateFeature(Feature feature);
        Task<Feature> UpdateFeature(string featureId, Feature feature);
        Task DeleteFeature(string featureId);
    }

    public class InMemoryFeatureService : IFeatureService
    {
        public static List<Feature> InMemoryDb { get; private set; } = new List<Feature>();

        public async Task<Feature> CreateFeature(Feature feature)
        {
            InMemoryDb.Add(feature);
            return feature;
        }

        public async Task DeleteFeature(string featureId)
        {
            InMemoryDb.RemoveAll(f => f.Id == featureId);
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
                throw new Exception("could not find feature");
            
            InMemoryDb.RemoveAll(f => f.Id == featureId);
            InMemoryDb.Add(feature);

            return feature;
        }
    }
}