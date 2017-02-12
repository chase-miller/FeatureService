using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FeatureService.Models.Domain;
using FeatureService.Persistence;

namespace FeatureService.Services
{
    public interface IFeatureService
    {
        Task<Feature> GetFeature(string featureId);
        Task<IEnumerable<Feature>> GetAllFeatures();
        Task<Feature> CreateFeature(Feature feature);
        Task<Feature> UpdateFeature(string featureId, Feature feature);
        Task<bool> DeleteFeature(string featureId);
    }

    public class FeatureServiceImpl : IFeatureService
    {
        private readonly IFeatureRepo _featureRepo;

        public FeatureServiceImpl(IFeatureRepo featureRepo)
        {
            _featureRepo = featureRepo;
        }

        public Task<Feature> CreateFeature(Feature feature)
        {
            return _featureRepo.CreateFeature(feature);
        }

        public Task<bool> DeleteFeature(string featureId)
        {
            return _featureRepo.DeleteFeature(featureId);
        }

        public Task<IEnumerable<Feature>> GetAllFeatures()
        {
            return _featureRepo.GetAllFeatures();
        }

        public Task<Feature> GetFeature(string featureId)
        {
            return _featureRepo.GetFeature(featureId);
        }

        public Task<Feature> UpdateFeature(string featureId, Feature feature)
        {
            return _featureRepo.UpdateFeature(featureId, feature);
        }
    }
}