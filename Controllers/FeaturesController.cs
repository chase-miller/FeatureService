using System.Linq;
using System.Threading.Tasks;
using FeatureService.Models.Api;
using FeatureService.Models.Domain;
using FeatureService.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FeatureService.Controllers
{
    [Route("api/[controller]")]
    public class FeaturesController : Controller
    {
        private IFeatureService _featureService;

        public FeaturesController(IFeatureService featureService)
        {
            _featureService = featureService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var features = await _featureService.GetAllFeatures();

            var responses = features.Select(f => new FeatureResponse
            {
                Id = f.Id,
                Enabled = f.Enabled,
                Created = f.Created,
                Lifetime = f.Lifetime,
                Expiration = f.Expiration
            });

            return Ok(responses);
        }

        // GET api/values/5
        [HttpGet("{featureId}", Name = "GetFeature")]
        public async Task<IActionResult> Get(string featureId)
        {
            var feature = await _featureService.GetFeature(featureId);

            var response = new FeatureResponse
            {
                Id = feature.Id,
                Enabled = feature.Enabled,
                Created = feature.Created,
                Lifetime = feature.Lifetime,
                Expiration = feature.Expiration
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]FeatureRequest feature)
        {
            var domainFeature = new Models.Domain.Feature
            {
                Id = feature.Id,
                Enabled = feature.Enabled,
                Lifetime = feature.Lifetime
            };

            var newFeature = await _featureService.CreateFeature(domainFeature);

            var response = new FeatureResponse
            {
                Id = newFeature.Id,
                Enabled = newFeature.Enabled,
                Created = newFeature.Created,
                Lifetime = newFeature.Lifetime,
                Expiration = newFeature.Expiration
            };

            return CreatedAtRoute("GetFeature", new { FeatureId = response.Id }, response);
        }

        [HttpPut("{featureId}")]
        public async Task<IActionResult> Put(string featureId, [FromBody]FeatureRequest feature)
        {
            var domainFeature = new Models.Domain.Feature
            {
                Id = feature.Id,
                Enabled = feature.Enabled,
                Lifetime = feature.Lifetime
            };

            var updatedFeature = await _featureService.UpdateFeature(featureId, domainFeature);

            var response = new FeatureResponse
            {
                Id = updatedFeature.Id,
                Enabled = updatedFeature.Enabled,
                Created = updatedFeature.Created,
                Lifetime = updatedFeature.Lifetime,
                Expiration = updatedFeature.Expiration
            };

            return Ok(response);
        }

        [HttpDelete("{featureId}")]
        public async Task<IActionResult> Delete(string featureId)
        {
            await _featureService.DeleteFeature(featureId);
            return Ok();
        }
    }
}
